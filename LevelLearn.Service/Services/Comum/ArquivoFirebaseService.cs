using Firebase.Auth;
using Firebase.Storage;
using LevelLearn.Domain.Utils.AppSettings;
using LevelLearn.Service.Interfaces.Comum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Comum
{
    public enum DiretoriosFirebase
    {
        Arquivos,
        Imagens,
        ImagensPerfilUsuario
    }

    public class ArquivoFirebaseService : IArquivoService
    {
        private readonly FirebaseSettings _firebaseSettings;
        private readonly FirebaseStorage _firebaseStorage;
        private readonly ILogger<ArquivoFirebaseService> _logger;

        #region Ctor

        public ArquivoFirebaseService(IOptions<AppSettings> appSettings, ILogger<ArquivoFirebaseService> logger)
        {
            _firebaseSettings = appSettings.Value.FirebaseSettings;

            var firebaseAuthLink = AutenticarAsync().Result;

            // Firebase Storage
            var options = new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuthLink.FirebaseToken),
                ThrowOnCancel = true
            };

            _firebaseStorage = new FirebaseStorage(_firebaseSettings.Bucket, options);
            _logger = logger;
        }

        #endregion

        private async Task<FirebaseAuthLink> AutenticarAsync()
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSettings.ApiKey));

            FirebaseAuthLink firebaseAuthLink = await auth.SignInWithEmailAndPasswordAsync(
                _firebaseSettings.AuthEmail,
                _firebaseSettings.AuthPassword
            );

            return firebaseAuthLink;
        }


        public async Task<string> ObterArquivo(DiretoriosFirebase diretorio, string nomeArquivo)
        {
            var downloadUrl = await _firebaseStorage
                .Child(diretorio.ToString())
                .Child(nomeArquivo)
                .GetDownloadUrlAsync();

            return downloadUrl;
        }

        public async Task<string> SalvarArquivo(Stream arquivo, DiretoriosFirebase diretorio, string nomeArquivo)
        {
            var cancellationToken = new CancellationTokenSource();

            var downloadUrl = await _firebaseStorage
                .Child(diretorio.ToString())
                .Child(nomeArquivo)
                .PutAsync(arquivo, cancellationToken.Token);

            return downloadUrl;
        }

        public async Task DeletarArquivo(DiretoriosFirebase diretorio, string nomeArquivo)
        {
            if (string.IsNullOrWhiteSpace(nomeArquivo)) return;

            await _firebaseStorage
                .Child(diretorio.ToString())
                .Child(nomeArquivo)
                .DeleteAsync();
        }

        public Stream RedimensionarImagem(IFormFile arquivoImagem, int altura = 256, int largura = 256)
        {
            using Stream inputStream = arquivoImagem.OpenReadStream();
            Stream outputStream = new MemoryStream();

            try
            {
                using (var image = Image.Load(inputStream))
                {
                    image.Mutate(x => x.Resize(
                        new ResizeOptions
                        {
                            Size = new Size(largura, altura),
                            Mode = ResizeMode.Max,
                        })
                    );

                    image.SaveAsJpeg(outputStream, new JpegEncoder() { Quality = 95 });
                }

                outputStream.Seek(0, SeekOrigin.Begin);

                return outputStream;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Erro Redimensionar Imagem");
                return inputStream; // Retorna a imagem original
            }
        }


    }
}
