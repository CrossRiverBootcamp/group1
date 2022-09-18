using Microsoft.EntityFrameworkCore;
using CustomerAccount.DAL.EF;
using CustomerAccount.DAL.Entities;
using ExtendedExceptions;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.DAL.Models;
using AutoMapper;
using System.Linq;

namespace CustomerAccount.DAL
{
    public class Storage : IStorage
    {

        private readonly IDbContextFactory<CustomerAccountDBContext> _factory;
        private readonly IMapper _mapper;

        public Storage(IDbContextFactory<CustomerAccountDBContext> factory, IMapper mapper)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _mapper = mapper;
        }
        #region Account
        public async Task<bool> CustomerExists(string email)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                return await context.Customers.AnyAsync(sub => sub.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }

        public async Task<bool> CreateCustomerAccount(CustomerModel customerModel, AccountData accountData)
        {
            using var context = _factory.CreateDbContext();

            try
            {
                Customer customer = _mapper.Map<CustomerModel, Customer>(customerModel);
                accountData.Customer = customer;

                await context.Customers.AddAsync(customer);
                await context.AccountDatas.AddAsync(accountData);

                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<AccountData> GetAccountData(Guid accountDataId)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                AccountData accountData = await context.AccountDatas
                 .Where(acc => acc.Id.Equals(accountDataId))
                 .Include(acc => acc.Customer)
                 .FirstAsync();

                return accountData ?? throw new KeyNotFoundException();
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
        public async Task<bool> SenderHasEnoughBalance(Guid accountId, int amount)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                var customerAccount = await context.AccountDatas.FindAsync(accountId);
                return customerAccount.Balance >= amount;
            }
            catch (Exception ex)
            {
                //includes if key not found
                throw new DBContextException(ex.Message);
            }
        }
        public async Task MakeBankTransferAndSaveOperationsToDB(Guid transactionId, Guid fromAccountId, Guid toAccountId, int amount)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                //we don't check here for nullability of the accounts
                //because we already have checked it in the previous funcs

                var fromAccount = await context.AccountDatas.FindAsync(fromAccountId);
                fromAccount.Balance -= amount;

                var toAccount = await context.AccountDatas.FindAsync(toAccountId);
                toAccount.Balance += amount;

                OperationData creditOperation = new OperationData()
                {
                    AccountId = fromAccountId,
                    TransactionId = transactionId,
                    IsCredit = true,
                    TransactionAmount = amount,
                    Balance = fromAccount.Balance,
                    OperationTime = DateTime.UtcNow
                };
                OperationData debitOperation = new OperationData()
                {
                    AccountId = toAccountId,
                    TransactionId = transactionId,
                    IsCredit = false,
                    TransactionAmount = amount,
                    Balance = toAccount.Balance,
                    OperationTime = DateTime.UtcNow
                };

                await context.Operations.AddAsync(creditOperation);
                await context.Operations.AddAsync(debitOperation);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }

        public async Task<bool> CustumerAccountExists(Guid accountId)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                return await context.AccountDatas.AnyAsync(account => account.Id.Equals(accountId));
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
        #endregion

        #region EmailVerification

        public async Task CreateEmailVerification(EmailVerificationModel emailVerificationModel)
        {
            using var context = _factory.CreateDbContext();

            try
            {
                //check if email in use
                bool isEmailInUse = await context.Customers.AnyAsync(sub => sub.Email.Equals(emailVerificationModel.Email));
                if (isEmailInUse)
                    throw new EmailInUseException();

                //if email was in verification use updates, otherwize inserts
                //doesn't take care of a doube use request
                EmailVerification newEmailVerification = _mapper.Map<EmailVerificationModel, EmailVerification>(emailVerificationModel);
                EmailVerification oldEmailVerification = await context.EmailVerifications.FindAsync(emailVerificationModel.Email);

                if (oldEmailVerification == null)
                    await context.EmailVerifications.AddAsync(newEmailVerification);
                else
                    context.Entry(oldEmailVerification).CurrentValues.SetValues(newEmailVerification);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
        public async Task<bool> ValidateCodeAndTime(string email, string verificationCode)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                //check if email in use
                if (await context.Customers.AnyAsync(sub => sub.Email.Equals(email)))
                    throw new EmailInUseException();

                EmailVerification ev = await context.EmailVerifications.FindAsync(email);

                if (ev.ExpirationTime < DateTime.UtcNow)
                    throw new VerificationCodeExpiredException();

                return ev.VerificationCode.Equals(verificationCode);
            }
            catch (Exception ex)
            {
                //includes if key not found
                throw new DBContextException(ex.Message);
            }
        }
        public async Task<int> UpdateAndGetNumOfResends(string email)
        {
            using var context = _factory.CreateDbContext();

            try
            {
                int CodeNum = ((await context.EmailVerifications.FindAsync(email)).CodeNum++);
                await context.SaveChangesAsync();
                return CodeNum;
            }
            catch (Exception ex)
            {
                //includes if key not found
                throw new DBContextException(ex.Message);
            }
        }
        public async Task DeleteExpiredRows()
        {
            using var context = _factory.CreateDbContext();

            try
            {
                var expired = await context.EmailVerifications.Where(x => x.ExpirationTime < DateTime.UtcNow).ToListAsync();
                if (expired.Any())
                {
                    context.EmailVerifications.RemoveRange(expired);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }

        public async Task<int> UpdateAndGetNumOfAttempts(string email)
        {
            using var context = _factory.CreateDbContext();

            try
            {
                int numOfAttempts = (await context.EmailVerifications.FindAsync(email)).NumOfAttemps++;
                await context.SaveChangesAsync();
                return numOfAttempts;
            }
            catch (Exception ex)
            {
                //includes if key not found
                throw new DBContextException(ex.Message);
            }
        }
        #endregion

        #region Login
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
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
        #endregion

        #region Operation
        public async Task<IEnumerable<OperationData>> GetMatchedOperations(List<Guid> operations)
        {
            try
            {
                using var context = _factory.CreateDbContext();

                //selects double amount rows...
                return await context.Operations.Where(op => operations.Contains(op.TransactionId)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }

        public async Task<int> GetCountOperations(Guid AccountId)
        {
            using var context = _factory.CreateDbContext();

            try
            {
                return (await context.Operations.Where(op => op.AccountId.Equals(AccountId)).ToListAsync()).Count;
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
        public async Task<IEnumerable<OperationData>> GetByPageAndAccountId(Guid AccountId, int PageNumber, int PageSize)
        {
            using var context = _factory.CreateDbContext();
            IEnumerable<OperationData> pagedData;

            try
            {
                pagedData = await context.Operations.Where(Operation => Operation.AccountId.Equals(AccountId))
                  .OrderByDescending(Operat => Operat.OperationTime)
                  .Skip((PageNumber - 1) * PageSize)
                  .Take(PageSize)
                  .ToListAsync();

                return pagedData ?? throw new KeyNotFoundException("data not found");
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }

        #endregion







    }
}
