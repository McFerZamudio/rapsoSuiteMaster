using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace rapsoSuiteMaster.Services
{
    public class emailServices
    {
        private readonly MimeMessage _email;
        private readonly emailConf _emailConf;
        private readonly IConfiguration _config;

        public emailServices(emailData emailData, emailConf emailConf)
        {
            _email = new();
            _email.From.Add(new MailboxAddress(emailData.fromUser, emailData.fromEmail));
            _email.To.Add(new MailboxAddress(emailData.toUser, emailData.toEmail));
            _email.Subject = emailData.subject;
            _email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailData.body };

            _emailConf = emailConf;
        }

        public async Task<taskResponse> sendEmail()
        {
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.CheckCertificateRevocation = false;
                    await smtp.ConnectAsync(_emailConf.smtp, _emailConf.port, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);

                    await smtp.AuthenticateAsync(_emailConf.userEmail, _emailConf.passEmail);
                    var resp = await smtp.SendAsync(_email);

                    await smtp.DisconnectAsync(true);

                    return (new taskResponse("sendEmail", "Ok", "Email send it !"));
                };
               

            }
            catch (Exception ex)
            {

                return (new taskResponse("sendEmail", "Error", ex.Message.ToString()));
            }

           
        }

        public async Task<taskResponse> testEmail()
        {
            emailData _emailData = new("kamren25@ethereal.email", "Karen de Zamudio", "zaycomercadeo@hotmail.com", "Carlos Zamudio", "titulo", "cuerpo 666 funcional");
            emailConf _emailConf = new("smtp.ethereal.email", 587, _config["emailAdmin:userEmail"], _config["emailAdmin:passEmail"]);

            emailServices _emailServices = new(_emailData, _emailConf);

            var _resp = await _emailServices.sendEmail();
            return _resp;
        }

    }
}
