using AutoMapper;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class AccountBL: IAccountBL
    {
        private readonly IMapper _mapper;
        private readonly IStorage _Storage;
        public AccountBL(IMapper mapper, IStorage storage)
        {
            _mapper = mapper;
            _Storage = storage;
        }
        public async Task<bool> CreateAccount(CustomerAccountDTO customerAccountDTO)
        {
            bool isExists = await _Storage.CustomerExists(customerAccountDTO.Email);
            if (isExists)
                return false;
            return await _Storage.CreateCustomerAccount(_mapper.Map<CustomerAccountDTO, Customer>(customerAccountDTO));
        }
        

        public async Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId)
        {
            return _mapper.Map<AccountData,CustomerAccountInfoDTO>( await _Storage.GetAccountData(accountId)) ;
        }

      


    }
}
