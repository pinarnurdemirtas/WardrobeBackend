using MailKit.Net.Smtp;
using MimeKit;

namespace WardrobeBackend.Services
{
    public class EmailService
    {
        private const string EmailSender = "pncpnc979@gmail.com";
        private const string EmailPassword = "vcrw lerx bgeb upgp";

        public async Task SendConfirmationEmail(string userEmail, string verificationUrl, string userDocument)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("DIGITAL WARDROBE", EmailSender));
            mimeMessage.To.Add(new MailboxAddress("Yeni Kullanıcı", userEmail));
            mimeMessage.Subject = "Hesap Doğrulaması Gerekiyor";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = $"DIGITAL WARDROBE Hesap Doğrulama: {verificationUrl}\n"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync(EmailSender, EmailPassword);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }

        public async Task SendAccountVerifiedEmail(string userEmail)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("DIGITAL WARDROBE", EmailSender));
            mimeMessage.To.Add(new MailboxAddress("Kullanıcı", userEmail));
            mimeMessage.Subject = "Hesabınız Başarıyla Doğrulandı";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = "Hesabınız başarıyla doğrulandı. Artık sisteme giriş yapabilirsiniz."
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync(EmailSender, EmailPassword);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}