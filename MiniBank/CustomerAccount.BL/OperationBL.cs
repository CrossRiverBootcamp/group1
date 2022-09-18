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
using System.Security.Claims;

namespace CustomerAccount.BL
{
    public class OperationBL : IOperationBL
    {
        private readonly IMapper _mapper;
        private readonly IStorage _storage;

        public OperationBL(IMapper mapper, IStorage storage)
        {
            _mapper = mapper;
            _storage = storage;
        }
        public async Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId,int PageNumber, int PageSize)
        {
            IEnumerable<OperationData> operations = await _storage.GetByPageAndAccountId(AccountId, PageNumber, PageSize);

            //create list with all TransactionIds
            List<Guid> operationsTransactionIds = new List<Guid>(); 
            foreach(var op in operations)
            {
                operationsTransactionIds.Add(op.TransactionId);
            }

            //get list of all partner operations
            IEnumerable<OperationData> partnerOperations = await _storage.GetMatchedOperations(operationsTransactionIds);

            //fill partnerTransactionId field
            foreach (var op in operations)
            {
                foreach(var partnerOp in partnerOperations)
                {
                    if(partnerOp.TransactionId.Equals(op.TransactionId) && partnerOp.Id!= op.AccountId)
                        op.TransactionPartnerAccountId = partnerOp.AccountId;
                }
            }

            return _mapper.Map<IEnumerable<OperationData>, IEnumerable<OperationDTO>>(operations);
        }

        public async Task<TransactionPartnerDetailsDTO> GetTransactionPartnerAccountInfo(Guid transactionPartnerAccountId)
        {
            return _mapper.Map<AccountData, TransactionPartnerDetailsDTO>(await _storage.GetAccountData(transactionPartnerAccountId));
        }
        public Task<int> GetCountOperations(Guid AccountId)
        {
            return _storage.GetCountOperations(AccountId);
        }
        public Guid GetAccountIDFromToken(ClaimsPrincipal User)
        {
            var accountID = User.Claims.First(x => x.Type.Equals("AccountID", StringComparison.InvariantCultureIgnoreCase)).Value;
            return Guid.Parse(accountID);
        }

    }
}
