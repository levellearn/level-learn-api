using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Service.Interfaces.Usuarios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
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

        public EmailService(IOptions<AppSettings> appSettings, IWebHostEnvironment env)
        {
            _appSettings = appSettings.Value;
            _env = env;
        }

        public async Task EnviarEmailCadastroProfessor(string email, string nome, string userId, string tokenEncoded)
        {
            var rotaAPI = $"/usuarios/confirmar-email?userId={userId}&confirmationToken={tokenEncoded}";
            var linkConfirmacao = _appSettings.ApiSettings.BaseUrl + rotaAPI;

            var assunto = $"Cadastro de Professor no sistema {_appSettings.EmailSettings.DisplayName}";
            var mensagem = "";

            string filePath = Path.Combine(_env.WebRootPath, "EmailTemplates/CadastroPessoa.html");

            using (var reader = new StreamReader(filePath))
            {
                mensagem = await reader.ReadToEndAsync();
            }
            mensagem = mensagem.Replace("{nome}", nome);
            mensagem = mensagem.Replace("{linkConfirmacao}", linkConfirmacao);

            await EnviarEmailAsync(email, assunto, mensagem);
        }

        public async Task EnviarEmailAsync(string email, string assunto, string mensagem)
        {
            if (!_appSettings.EmailSettings.EnvioHabilitado) return;

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

    }


}
