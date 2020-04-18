using Firebase.Auth;
using Firebase.Storage;
using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Service.Interfaces.Comum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Comum
{
    public class ArquivoFirebaseService : IArquivoService
    {
        #region Atributos

        private readonly FirebaseSettings _firebaseSettings;
        private readonly FirebaseStorage _firebaseStorage;
        private const string DIRETORIO_IMAGEM = "Imagens";
        private const string DIRETORIO_ARQUIVO = "Arquivos";

        #endregion

        #region Ctor

        public ArquivoFirebaseService(IOptions<AppSettings> appSettings)
        {
            _firebaseSettings = appSettings.Value.FirebaseSettings;

            var firebaseAuthLink = AutenticarAsync().Result;

            var options = new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuthLink.FirebaseToken),
                ThrowOnCancel = true
            };

            _firebaseStorage = new FirebaseStorage(_firebaseSettings.Bucket, options);
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

        public async Task<string> ObterImagem(string nomeArquivo)
        {
            var downloadUrl = await _firebaseStorage
                .Child(DIRETORIO_IMAGEM)
                .Child(nomeArquivo)
                .GetDownloadUrlAsync();

            return downloadUrl;
        }

        public Task<string> ObterArquivo(string nomeArquivo)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SalvarArquivo(IFormFile arquivo, string diretorio)
        {
            var cancellationToken = new CancellationTokenSource();

            var downloadUrl = await _firebaseStorage
                .Child(diretorio)
                .Child(arquivo.FileName)
                .PutAsync(arquivo.OpenReadStream(), cancellationToken.Token);

            return downloadUrl;
        }

        public Task DeletarArquivo()
        {
            throw new NotImplementedException();
        }


    }
}
