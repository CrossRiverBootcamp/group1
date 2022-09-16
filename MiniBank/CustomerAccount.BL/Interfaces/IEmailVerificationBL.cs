using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface IEmailVerificationBL
    {
        Task HandleEmailVerificationRequest(string email, bool isResendRequest);
        void SendEmail(string email, string subject, string body);
        Task UpdateAndLimitNumberOfAttempts(string email);
        //private?!
        //Task<int> UpdateLimitAndReturnNumberOfResends(string email);
        Task<bool> ValidateCodeAndTime(CustomerDTO customerDTO);
        Task DeleteExpiredRows();
    }
}