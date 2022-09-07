using CustomerAccount.DAL.Entities;

namespace CustomerAccount.DAL
{
    public interface IStorage
    {
        Task<bool> CreateCustomerAccount(Customer customer, AccountData accountData);
        Task<bool> CustomerExists(string email);
        Task<AccountData> GetAccountData(Guid accountDataId);
        Task<bool> CustumerAccountExists(Guid accountId);
        Task<Guid> Login(string email, string password);
        Task<bool> SenderHasEnoughBalance(Guid accountId, int amount);
        Task MakeBankTransfer(Guid fromAccountId, Guid toAccountId, int amount);
        //Task<Customer> GetCustomer(Guid customerId);
    }
}