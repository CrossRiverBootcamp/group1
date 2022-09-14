using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Models;

namespace CustomerAccount.DAL.Interfaces
{
    public interface ICustomerAccountDAL
    {
        Task<bool> CustomerExists(string email);
        Task<bool> ValidateCodeAndTime(string email, string verificationCode);
        Task CreatesEmailVerification(EmailVerificationModel emailVerificationModel);
        Task<bool> CreateCustomerAccount(Customer customer, AccountData accountData);
        Task<AccountData> GetAccountData(Guid accountDataId);
        Task<bool> CustumerAccountExists(Guid accountId);
        Task<Guid> Login(string email, string password);
        Task<bool> SenderHasEnoughBalance(Guid accountId, int amount);
        Task<BalancesModel> MakeBankTransfer(Guid fromAccountId, Guid toAccountId, int amount);
        //Task<Customer> GetCustomer(Guid customerId);
    }
}