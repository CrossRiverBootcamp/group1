using Microsoft.EntityFrameworkCore;
using Transaction.DAL.EF;
using Transaction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedExceptions;
using Transaction.DAL.Models;
using Transaction.DTO;

namespace Transaction.DAL
{
    public class Storage : IStorage
    {
        private readonly IDbContextFactory<TransactionDBContext> _factory;
        public Storage(IDbContextFactory<TransactionDBContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        public async Task<Guid> PostTransaction(Entities.Transaction transaction)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                var transactionId = (await context.Transactions.AddAsync(transaction)).Entity.Id;
                await context.SaveChangesAsync();
                return transactionId;
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
        public async Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO)
        {
           using var context = _factory.CreateDbContext();

            try
            {
                Entities.Transaction transaction = await context.Transactions.FindAsync(upadateTransactionStatusDTO.TransactioId);

                transaction.Status = upadateTransactionStatusDTO.IsSuccess ? StatusEnum.SUCCESS : StatusEnum.FAIL;
                transaction.FailureReason = upadateTransactionStatusDTO.FailureReasun;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //includes if key not found
                throw new DBContextException(ex.Message);
            }
        }
    }
}
