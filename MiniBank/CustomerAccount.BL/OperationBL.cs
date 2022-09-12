using AutoMapper;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.DTO;
using Transaction.Messeges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class OperationBL : IOperationBL
    {
        private readonly IMapper _mapper;
        private readonly IOperationDAL _operationDAL;
        public OperationBL(IMapper mapper, IOperationDAL operationDAL)
        {
            _mapper = mapper;
            _operationDAL = operationDAL;
        }
        public async Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId, int PageNumber, int PageSize)
        {
            return _mapper.Map<IEnumerable<OperationData>, IEnumerable<OperationDTO>>(await _operationDAL.GetByPageAndAccountId(AccountId, PageNumber, PageSize));

        }
        public async Task<bool> PostOperation(MakeTransfer makeTransfer)
        {
           
            OperationData creditOperation = new OperationData()
            {
                AccountId = makeTransfer.FromAccountId,
                TransactionId = makeTransfer.TransactionId,
                IsCredit = true,
                TransactionAmount = makeTransfer.Amount,
                //balance
                OperationTime = DateTime.UtcNow
            };
            OperationData debitOperation = new OperationData()
            {
                AccountId = makeTransfer.ToAccountId,
                TransactionId = makeTransfer.TransactionId,
                IsCredit = false,
                TransactionAmount = makeTransfer.Amount,
                //balance
                OperationTime = DateTime.UtcNow
            };

            try
            {
                await _operationDAL.PostOperation(creditOperation);
                await _operationDAL.PostOperation(debitOperation);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
