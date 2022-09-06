using Microsoft.EntityFrameworkCore;
using CustomerAccount.DAL.EF;
using CustomerAccount.DAL.Entities;
using ExtendedExceptions;

namespace CustomerAccount.DAL
{
    public class Storage : IStorage
    {
        private readonly IDbContextFactory<CustomerAccountDBContext> _factory;
        public Storage(IDbContextFactory<CustomerAccountDBContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        public async Task<bool> CustomerExists(string email)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                return await context.Customers.AnyAsync(sub => sub.Email.Equals(email));
            }
            catch(Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
        public async Task<bool> CreateCustomerAccount(Customer customer, AccountData accountData)
        {
            using var context = _factory.CreateDbContext();

            try
            {
                await context.Customers.AddAsync(customer);
                await context.AccountDatas.AddAsync(accountData);

                await context.SaveChangesAsync();
            }
            catch
            {
                // throw new CreateUserException();
                return false;
            }
            return true;
        }
        public async Task<Guid> Login(string email, string password)
        {
            using var context = _factory.CreateDbContext();
            try
            {
               AccountData accountData = await context.AccountDatas
                .Where(acc => acc.Customer.Email.Equals(email) && acc.Customer.Password.Equals(password))
                .Include(acc => acc.Customer)
                .FirstOrDefaultAsync();

               return accountData?.Id ?? throw new UnauthorizedAccessException("Login failed, your name or password are not correct:(");
            }
            catch(Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }

        public async Task<AccountData> GetAccountData(Guid accountDataId)
        {
            using var context = _factory.CreateDbContext();
            try
            {
               return await context.AccountDatas
                .Where(acc => acc.Id.Equals(accountDataId))
                .Include(acc => acc.Customer)
                .FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
               throw new DBContextException(ex.Message);
            }
        }
    }
}
