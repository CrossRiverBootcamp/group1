using System.Net.Mail;
using System.Net;
using CustomerAccount.BL.Options;

namespace EmailSender.Service
{
    public class SendsEmail : ISendsEmail
    {
        public void SendEmail(EmailOptions _options, string RecieverEmail, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_options.Email);
                mail.To.Add(RecieverEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(_options.Email, _options.Password);
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}