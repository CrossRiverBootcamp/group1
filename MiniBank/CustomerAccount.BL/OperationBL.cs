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
        private readonly ICustomerAccountDAL _CustomerAccountDAL;

        public OperationBL(IMapper mapper, IOperationDAL operationDAL, ICustomerAccountDAL CustomerAccountDAL)
        {
            _mapper = mapper;
            _operationDAL = operationDAL;
            _CustomerAccountDAL = CustomerAccountDAL;
        }
        public async Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId,int PageNumber, int PageSize)
        {
            return _mapper.Map<IEnumerable<OperationData>, IEnumerable<OperationDTO>>(await _operationDAL.GetByPageAndAccountId(AccountId, PageNumber, PageSize));

        }
        public async Task PostOperations(MakeTransfer makeTransferMsg, BalancesDTO balances)
        {
           
            OperationData creditOperation = new OperationData()
            {
                AccountId = makeTransferMsg.FromAccountId,
                TransactionId = makeTransferMsg.TransactionId,
                IsCredit = true,
                TransactionAmount = makeTransferMsg.Amount,
                Balance=balances.FromAccountBalance,
                OperationTime = DateTime.UtcNow
            };
            OperationData debitOperation = new OperationData()
            {
                AccountId = makeTransferMsg.ToAccountId,
                TransactionId = makeTransferMsg.TransactionId,
                IsCredit = false,
                TransactionAmount = makeTransferMsg.Amount,
                Balance=balances.ToAccountBalance,
                OperationTime = DateTime.UtcNow
            };
           
             await _operationDAL.PostOperation(creditOperation);
             await _operationDAL.PostOperation(debitOperation);

        }
        public async Task<TransactionPartnerDetailsDTO> GetTransactionPartnerAccountInfo(Guid transactionPartnerAccountId)
        {
            return _mapper.Map<AccountData, TransactionPartnerDetailsDTO>(await _CustomerAccountDAL.GetAccountData(transactionPartnerAccountId));
        }
    }
}
