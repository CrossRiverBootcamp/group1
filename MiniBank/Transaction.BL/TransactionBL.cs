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
        private readonly ITransactionDAL _TransactionDAL;
        private readonly IMapper _mapper;
  
        public TransactionBL(IMapper mapper, ITransactionDAL TransactionDAL)
        {
            _mapper = mapper;
            _TransactionDAL = TransactionDAL;
            
        }
        
        public async Task<bool> PostTransactionStartSaga(TransactionDTO transactionDTO, IMessageSession _messageSession)
        {
            
            DAL.Entities.Transaction transaction = _mapper.Map<TransactionDTO, DAL.Entities.Transaction>(transactionDTO);
            transaction.Date = DateTime.UtcNow;

            try
            {
                var transactionId = await _TransactionDAL.PostTransaction(transaction);
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
            _TransactionDAL.ChangeTransactionStatus(upadateTransactionStatusDTO);
        }

    }
}
