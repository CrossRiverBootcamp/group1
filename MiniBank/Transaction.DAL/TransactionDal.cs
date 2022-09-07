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
    public class TransactionDal : ITransactionDal
    {
        private readonly IDbContextFactory<TransactionDBContext> _factory;
        public TransactionDal(IDbContextFactory<TransactionDBContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<bool> PostTransaction(Entities.Transaction transaction)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DBContextException(ex.Message);
            }
        }
        public async Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO)
        {
           using var context = _factory.CreateDbContext();

            Entities.Transaction transaction = await context.Transactions.FindAsync(upadateTransactionStatusDTO.TransactioId)
                ?? throw new KeyNotFoundException("Transaction to update not found");
            //האם מיותר האקספשן?

            transaction.Status = upadateTransactionStatusDTO.Status;
            transaction.FailureReason = upadateTransactionStatusDTO.FailureReasun;
            await context.SaveChangesAsync();
        }
    }
}
