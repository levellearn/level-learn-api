using Firebase.Auth;
using Firebase.Storage;
using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Service.Interfaces.Comum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Comum
{
    public class ArquivoFirebaseService : IArquivoService
    {
        private readonly FirebaseSettings _firebaseSettings;
        private Task<FirebaseAuthLink> _firebaseAuthLink;

        public ArquivoFirebaseService(IOptions<AppSettings> appSettings)
        {
            _firebaseSettings = appSettings.Value.FirebaseSettings;

        }

        private void Autenticar()
        {
            if (_firebaseAuthLink != null)
                return;

            var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSettings.ApiKey));
            _firebaseAuthLink = auth.SignInWithEmailAndPasswordAsync(_firebaseSettings.AuthEmail, _firebaseSettings.AuthPassword);
        }

        public async Task<string> ObterArquivo(string dir, string nomeArquivo)
        {
            Autenticar();

            string downloadUrl = await new FirebaseStorage
                (
                     _firebaseSettings.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(_firebaseAuthLink.Result.FirebaseToken),
                        ThrowOnCancel = true
                    }
                )
                .Child(dir)
                .Child(nomeArquivo)
                .GetDownloadUrlAsync();

            //task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            return downloadUrl;
        }

        public async Task SalvarArquivo(IFormFile formFile, string dir)
        {
            Autenticar();

            // Get any Stream - it can be FileStream, MemoryStream or any other type of Stream
            //var stream = File.Open(@"C:\Users\you\file.png", FileMode.Open);

            var cancellationToken = new CancellationTokenSource();

            var upload = await new FirebaseStorage
                (
                    _firebaseSettings.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(_firebaseAuthLink.Result.FirebaseToken),
                        ThrowOnCancel = true
                    }
                )
                .Child(dir)
                .Child(formFile.FileName)
                .PutAsync(formFile.OpenReadStream(), cancellationToken.Token);
        }



        public Task DeletarArquivo()
        {
            throw new NotImplementedException();
        }


    }
}
