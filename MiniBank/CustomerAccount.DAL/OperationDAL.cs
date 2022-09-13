using CustomerAccount.DAL.EF;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Interfaces;
using ExtendedExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL
{
    public class OperationDAL : IOperationDAL
    {
        private readonly IDbContextFactory<CustomerAccountDBContext> _factory;
        public OperationDAL(IDbContextFactory<CustomerAccountDBContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        public async Task<IEnumerable<OperationData>> GetByPageAndAccountId(Guid AccountId, int PageNumber, int PageSize)
        {
            using var context = _factory.CreateDbContext();
            IEnumerable<OperationData> pagedData;

           // if (sortDirection.Equals(SortDirection.Ascending))
            //{
            //    pagedData = await context.Operations.Where(Operation => Operation.AccountId == AccountId)
            //     .OrderBy(Operat => Operat.OperationTime)
            //     .Skip((PageNumber - 1) * PageSize)
            //     .Take(PageSize)
            //     .ToListAsync();
            //}
            //else
            //{
                pagedData = await context.Operations.Where(Operation => Operation.AccountId.Equals(AccountId))
                 .OrderByDescending(Operat => Operat.OperationTime)
                 .Skip((PageNumber - 1) * PageSize)
                 .Take(PageSize)
                 .ToListAsync();
            //}

            return pagedData ?? throw new KeyNotFoundException("data not found");
        }
        public async Task PostOperation(OperationData operation)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                await context.Operations.AddAsync(operation);
                await context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
    }
}
