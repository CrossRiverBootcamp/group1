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
        private readonly IStorage _storage;

        public OperationBL(IMapper mapper, IStorage storage)
        {
            _mapper = mapper;
            _storage = storage;
        }
        public async Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId,int PageNumber, int PageSize)
        {
            return _mapper.Map<IEnumerable<OperationData>, IEnumerable<OperationDTO>>(await _storage.GetByPageAndAccountId(AccountId, PageNumber, PageSize));
        }

        public async Task<TransactionPartnerDetailsDTO> GetTransactionPartnerAccountInfo(Guid transactionPartnerAccountId)
        {
            return _mapper.Map<AccountData, TransactionPartnerDetailsDTO>(await _storage.GetAccountData(transactionPartnerAccountId));
        }
    }
}
