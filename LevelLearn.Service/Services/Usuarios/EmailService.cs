using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Service.Interfaces.Usuarios;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
{
    public class EmailService : IEmailService
    {
        private const string NOME_APLICACAO = "Level Learn: um ambiente gratuito de gamification.";

        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task EnviarEmailCadastroProfessor(string email, string nome, string userId, string confirmationToken)
        {
            var confirmationLink = $"{_appSettings.ApiSettings.BaseUrl}/usuarios/confirmar-email?userId={userId}&confirmationToken={confirmationToken}";

            var assunto = $"Cadastro de Professor no sistema {_appSettings.EmailSettings.DisplayName}";
            var mensagem = @$"<h1>Olá {nome}, Bem-vindo ao {NOME_APLICACAO}</h1> 
                           Clique no link de confirmação de email: {confirmationLink}";

            await EnviarEmailAsync(email, assunto, mensagem);
        }

        public async Task EnviarEmailAsync(string email, string assunto, string mensagem)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_appSettings.EmailSettings.Email, _appSettings.EmailSettings.DisplayName),
                Subject = assunto,
                Body = mensagem,
                IsBodyHtml = true,
                Priority = MailPriority.High,
            };

            mail.To.Add(new MailAddress(email));

            using (var smtp = new SmtpClient(_appSettings.EmailSettings.Host, _appSettings.EmailSettings.Porta))
            {
                smtp.Credentials = new NetworkCredential(_appSettings.EmailSettings.Email, _appSettings.EmailSettings.Senha);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }

    }


}
