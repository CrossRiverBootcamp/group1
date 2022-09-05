using CustomerAccount.DAL.Entities;

namespace CustomerAccount.DAL
{
    public interface IStorage
    {
        Task<bool> CreateCustomerAccount(Customer customer);
        Task<bool> CustomerExists(string email);
        Task<AccountData> GetAccountData(Guid accountDataId);
        //Task<Customer> GetCustomer(Guid customerId);
        Task<Guid> Login(string email, string password);
    }
}