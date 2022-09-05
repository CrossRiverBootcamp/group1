using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAccount.DAL.EF;
using CustomerAccount.DAL.Entities;

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

            //if (context.Customers == null)
            //  return false;
            return await context.Customers.AnyAsync(sub => sub.Email.Equals(email));
        }
        public async Task<bool> CreateCustomerAccount(Customer customer)
        {
            using var context = _factory.CreateDbContext();

            try
            {
                await context.Customers.AddAsync(customer);

                AccountData accountData = new AccountData()
                {
                    Customer = customer,
                    OpenDate = DateTime.UtcNow,
                    Balance = "1000"
                };
                await context.AccountDatas.AddAsync(accountData);

                await context.SaveChangesAsync();
            }
            catch
            {
                //throw new Exeption("")
                return false;
            }
            return true;
        }
        public async Task<Guid> Login(string email, string password)
        {
            using var context = _factory.CreateDbContext();

            AccountData accountData = await context.AccountDatas
                .Include(acc => acc.Customer)
                .FirstOrDefaultAsync(acc =>
                acc.Customer.Email.Equals(email) && acc.Customer.Password.Equals(password));

            return accountData?.Id ?? throw new UnauthorizedAccessException("Login failed");
        }

        public async Task<AccountData> GetAccountData(Guid accountDataId)
        {
            using var context = _factory.CreateDbContext();
            return await context.AccountDatas
                .Where(acc => acc.Id.Equals(accountDataId))
                .Include(acc => acc.Customer)
                .FirstOrDefaultAsync();

            //return await context.AccountDatas.FindAsync(accountDataId);
        }

        //public async Task<Customer> GetCustomer(Guid customerId)
        //{
        //    using var context = _factory.CreateDbContext();

        //    return await context.Customers.FindAsync(customerId);
        //}
    }
}
