using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Models;

namespace CustomerAccount.DAL.Interfaces
{
    public interface IStorage
    {
        #region Account

        Task<bool> CustomerExists(string email);
        Task<bool> CreateCustomerAccount(CustomerModel customerModel, AccountData accountData);
        Task<AccountData> GetAccountData(Guid accountDataId);
        Task<bool> SenderHasEnoughBalance(Guid accountId, int amount);
        Task MakeBankTransferAndSaveOperationsToDB(Guid transactionId, Guid fromAccountId, Guid toAccountId, int amount);
        Task<bool> CustumerAccountExists(Guid accountId);
        #endregion

        #region EmailVerification

      
        Task CreateEmailVerification(EmailVerificationModel emailVerificationModel);
        Task<bool> ValidateCodeAndTime(string email, string verificationCode);
        Task<int> UpdateAndGetNumOfAttempts(string email);
        Task<int> UpdateAndGetNumOfResends(string email);
        Task DeleteExpiredRows();
        #endregion
       
        #region Login
        Task<Guid> Login(string email, string password);

        #endregion
       
        #region Operation
        Task<IEnumerable<OperationData>> GetByPageAndAccountId(Guid AccountId, int PageNumber, int PageSize);
        Task<int> GetCountOperations(Guid AccountId);
        Task<IEnumerable<OperationData>> GetMatchedOperations(List<Guid> operations);
        #endregion


    }
}