using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Utils.AppSettings;
using LevelLearn.Service.Interfaces.Usuarios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<AppSettings> appSettings, IWebHostEnvironment env, ILogger<EmailService> logger)
        {
            _appSettings = appSettings.Value;
            _env = env;
            _logger = logger;
        }

        public async Task EnviarEmailAsync(string email, string assunto, string mensagem)
        {
            // TODO: Tirar comentario
            //if (!_appSettings.EmailSettings.EnvioHabilitado) return;

            var mail = new MailMessage()
            {
                From = new MailAddress(_appSettings.EmailSettings.Email, _appSettings.EmailSettings.DisplayName),
                Subject = assunto,
                Body = mensagem,
                IsBodyHtml = true,
                Priority = MailPriority.High,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
            };
            mail.To.Add(new MailAddress(email));

            using (var smtp = new SmtpClient(_appSettings.EmailSettings.Host, _appSettings.EmailSettings.Porta))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_appSettings.EmailSettings.Email, _appSettings.EmailSettings.Senha);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }

        public async Task EnviarEmailCadastro(string email, string nome, string userId, string tokenEncoded, TipoPessoa tipoPessoa)
        {
            string rotaAPI = $"/usuarios/confirmar-email?userId={userId}&confirmationToken={tokenEncoded}";
            string linkConfirmacao = _appSettings.ApiSettings.BaseUrl + rotaAPI;

            string assunto = $"Cadastro de {tipoPessoa} no sistema {_appSettings.EmailSettings.DisplayName}";
            string mensagem = "";

            string filePath = Path.Combine(_env.WebRootPath, "EmailTemplates/CadastroPessoa.html");

            using (var reader = new StreamReader(filePath))
                mensagem = await reader.ReadToEndAsync();

            mensagem = mensagem.Replace("{nome}", nome);
            mensagem = mensagem.Replace("{linkConfirmacao}", linkConfirmacao);

            await EnviarEmailAsync(email, assunto, mensagem);
        }

        public async Task EnviarEmailRedefinirSenha(string email, string nome, string userId, string resetToken)
        {
            try
            {
                string rotaAPI = $"/usuarios/redefinir-senha?userId={userId}&resetToken={resetToken}";
                string linkRedefinirSenha = _appSettings.ApiSettings.BaseUrl + rotaAPI;

                string assunto = $"Redefinição de senha no sistema {_appSettings.EmailSettings.DisplayName}";
                string mensagem = "";

                string filePath = Path.Combine(_env.WebRootPath, "EmailTemplates/RedefinirSenha.html");

                using (var reader = new StreamReader(filePath))
                    mensagem = await reader.ReadToEndAsync();

                mensagem = mensagem.Replace("{nome}", nome);
                mensagem = mensagem.Replace("{email}", email);
                mensagem = mensagem.Replace("{linkRedefinirSenha}", linkRedefinirSenha);

                await EnviarEmailAsync(email, assunto, mensagem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro Enviar Email Redefinir Senha");
                throw;
            }
            
        }


    }
}
