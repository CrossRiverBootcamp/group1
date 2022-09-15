namespace CustomerAccount.BL.Interfaces
{
    public interface IEmailVerificationBL
    {
        Task HandleEmailVerificationRequest(string email);
        Task SendEmail(string email, string subject, string body);
    }
}