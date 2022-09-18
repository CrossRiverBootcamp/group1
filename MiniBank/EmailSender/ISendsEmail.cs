using CustomerAccount.BL.Options;

namespace EmailSender.Service
{
    public interface ISendsEmail
    {
        void SendEmail(EmailOptions _options, string RecieverEmail, string subject, string body);
    }
}