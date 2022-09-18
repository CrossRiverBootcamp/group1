using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface IEmailVerificationBL
    {
        Task HandleEmailVerificationRequest(string email, bool isResendRequest);
        Task<int> UpdateAndLimitNumberOfAttempts(string email);
        Task<bool> ValidateCodeAndTime(CustomerDTO customerDTO);
        Task DeleteExpiredRows();

    }
}