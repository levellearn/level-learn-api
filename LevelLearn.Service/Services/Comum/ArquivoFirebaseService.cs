using Firebase.Auth;
using Firebase.Storage;
using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Service.Interfaces.Comum;
using Microsoft.Extensions.Options;
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

        #region Ctor

        public ArquivoFirebaseService(IOptions<AppSettings> appSettings)
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


    }
}
