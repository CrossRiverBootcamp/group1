using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.EF;
using Transaction.DAL.Entities;
using AutoMapper;
using Transaction.DTO;
using Transaction.DAL;
using Transaction.DAL.Models;
using Transaction.BL.Interfaces;
using NServiceBus;
using Transaction.Messeges;
using ExtendedExceptions;

namespace Transaction.BL
{
    public class TransactionBL : ITransactionBL
    {
        private readonly ITransactionDal _transactionDal;
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;
        public TransactionBL(IMapper mapper, IMessageSession messageSession, ITransactionDal transactionDal)
        {
            _mapper = mapper;
            _transactionDal = transactionDal;
            _messageSession = messageSession;
        }
        
        public async Task<bool> PostTransactionStartSaga(TransactionDTO transactionDTO)
        {
            
            DAL.Entities.Transaction transaction = _mapper.Map<TransactionDTO, DAL.Entities.Transaction>(transactionDTO);
            transaction.Date = DateTime.UtcNow;

            try
            {
                var transactionId = await _transactionDal.PostTransaction(transaction);
                TransactionReqMade transactionReqMade = new TransactionReqMade()
                {
                    TransactionId = transactionId,
                    FromAccountId = transaction.FromAccountId,
                    ToAccountId = transaction.ToAccountId,
                    Amount = transaction.Amount
                };

                //publish the event
                await _messageSession.Publish(transactionReqMade);
                return true;
            }
            catch
            {
                return false;
            }         
        }
        public async Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO)
        {
            _transactionDal.ChangeTransactionStatus(upadateTransactionStatusDTO);
        }

    }
}
