using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class AccountBL
    {
        private readonly IMapper _mapper;
        private readonly IStorage _Storage;
        public AccountBL(IMapper mapper, IStorage storage)
        {
            _mapper = mapper;
            _Storage = storage;
        }
        Task<bool> CreateAccount(Guid Email)
        {
            if (!_Storage.CreateCustomer(Email))
                return ErrorEventArgs;
            return _Storage.CreateCustomerAccount(_mapper.Map<AccountCustomerDTO, Customer>(accountCustomerDTO));
        }
        

        Task<AccountCustomerInfoDTO> GetAccountInfo(accountId)
        {
            return _mapper.Map<AccountCustomerDTO,>(_Storage.GetAccountData(accountId)) ;
        }

      


    }
}
