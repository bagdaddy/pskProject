using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage);

        Task SendAdminEmailAsync(string subject, string textMessage);
    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailSender(IOptions<EmailSettings> emailSettings, IHttpContextAccessor httpContextAccessor)
        {
            _emailSettings = emailSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(email));

            mimeMessage.Subject = subject;
            var builder = new BodyBuilder { TextBody = textMessage, HtmlBody = htmlMessage };
            mimeMessage.Body = builder.ToMessageBody();

            try
            {
                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                if (_emailSettings.IsDevelopment)
                    await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, _emailSettings.UseSsl)
                        .ConfigureAwait(false);
                else
                    await client.ConnectAsync(_emailSettings.MailServer).ConfigureAwait(false);

                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password).ConfigureAwait(false);
                await client.SendAsync(mimeMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task SendAdminEmailAsync(string subject, string textMessage)
        {
            var _context = _httpContextAccessor.HttpContext;
            var _host = _context.Request.Host.ToString();
            var _uaString = _context.Request.Headers["User-Agent"].ToString();
            var _ipAnonymizedString = _context.Connection.RemoteIpAddress.AnonymizeIP();
            var _uid = _context.User.Identity.IsAuthenticated
                ? _context.User.FindFirst(ClaimTypes.NameIdentifier).Value
                : "Unknown";
            var _path = _context.Request.Path;
            var _queryString = _context.Request.QueryString;

            StringBuilder sb = new StringBuilder($"Host = {_host} \r\n");
            sb.Append($"User Agent = {_uaString} \r\n");
            sb.Append($"Anonymized IP = {_ipAnonymizedString} \r\n");
            sb.Append($"UserId = {_uid} \r\n");
            sb.Append($"Path = {_path} \r\n");
            sb.Append($"QueryString = {_queryString} \r\n \r\n");
            sb.Append(textMessage);

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(_emailSettings.AdminEmail));

            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain") { Text = sb.ToString() };

            try
            {
                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                if (_emailSettings.IsDevelopment)
                    await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, _emailSettings.UseSsl)
                        .ConfigureAwait(false);
                else
                    await client.ConnectAsync(_emailSettings.MailServer).ConfigureAwait(false);

                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password).ConfigureAwait(false);
                await client.SendAsync(mimeMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
