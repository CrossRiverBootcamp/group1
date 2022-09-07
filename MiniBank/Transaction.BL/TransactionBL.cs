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

namespace Transaction.BL
{
    public class TransactionBL : ITransactionBL
    {
        private readonly ITransactionDal _transactionDal;
        private readonly IMapper _mapper;

        public TransactionBL(IMapper mapper, ITransactionDal transactionDal)
        {
            _mapper = mapper;
            _transactionDal = transactionDal;
        }
        public async Task<bool> PostTransactionStartSaga(TransactionDTO transactionDTO)
        {
            
            DAL.Entities.Transaction transaction = _mapper.Map<TransactionDTO, DAL.Entities.Transaction>(transactionDTO);
            transaction.Date = DateTime.UtcNow;

            bool isSuccess = await _transactionDal.PostTransaction(transaction);
            if (!isSuccess)
                return false;

            //publish the event
            return true;
        }
        public async Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO)
        {
            _transactionDal.ChangeTransactionStatus(upadateTransactionStatusDTO);
        }
    }
}
