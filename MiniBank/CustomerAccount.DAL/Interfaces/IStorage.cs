using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Models;

namespace CustomerAccount.DAL.Interfaces
{
    public interface IStorage
    {
        Task<bool> CreateCustomerAccount(CustomerModel customerModel, AccountData accountData);
        Task CreateEmailVerification(EmailVerificationModel emailVerificationModel);
        Task<bool> CustomerExists(string email);
        Task<bool> CustumerAccountExists(Guid accountId);
        Task<AccountData> GetAccountData(Guid accountDataId);
      
        Task MakeBankTransferAndSaveOperationsToDB(Guid transactionId,Guid fromAccountId, Guid toAccountId, int amount);
        Task<bool> SenderHasEnoughBalance(Guid accountId, int amount);
        Task<int> UpdateAndGetNumOfAttempts(string email);
        Task<int> UpdateAndGetNumOfResends(string email);
        Task<bool> ValidateCodeAndTime(string email, string verificationCode);

        //FOR: Login BL
        Task<Guid> Login(string email, string password);

        //FOR: operation BL
        Task<IEnumerable<OperationData>> GetByPageAndAccountId(Guid AccountId, int PageNumber, int PageSize);
        Task<bool> DeleteExpiredRows();
    }
}