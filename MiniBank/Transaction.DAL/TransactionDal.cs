using Microsoft.EntityFrameworkCore;
using Transaction.DAL.EF;
using Transaction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedExceptions;

namespace Transaction.DAL
{
    public class TransactionDal : ITransactionDal
    {
        private readonly IDbContextFactory<TransactionDBContext> _factory;
        public TransactionDal(IDbContextFactory<TransactionDBContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<bool> PostTransaction(Entities.Transaction Transaction)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                await context.Transactions.AddAsync(Transaction);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
    }
}
