using CustomerAccount.DAL.EF;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Interfaces;
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

            var pagedData = await context.Operations.Where(Operation => Operation.AccountId == AccountId).OrderBy(Operat => Operat.OperationTime)
               .Skip((PageNumber - 1) * PageSize)
               .Take(PageSize)
               .ToListAsync();

            return pagedData ?? throw new KeyNotFoundException("data not found");
        }
    }
}
