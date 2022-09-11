using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL
{
   public class OperationDAL
    {
        private readonly IDbContextFactory<OperationDBContext> _factory;
        public OperationDAL(IDbContextFactory<OperationDBContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        public async Task<IEnumerable<Operation>> GetByPageAndAccountId(int AccountId, int PageNumber, int PageSize)
        {
            using var context = _factory.CreateDbContext();


            var pagedData = await context.Operations.Where(Operation => Operation.AccountId == AccountId).OrderBy(Operat => Operat.date)
               .Skip((PageNumber - 1) * PageSize)
               .Take(PageSize)
               .ToListAsync();


            return pagedData ?? throw new KeyNotFoundException("data not found");
        }
    }
}
