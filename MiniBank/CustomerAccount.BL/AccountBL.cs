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
        public async Task<bool> CreateAccount(CustomerDTO customerDTO)
        {
            bool isExists = await _Storage.CustomerExists(customerDTO.Email);
            if (isExists)
                return false;

            Customer customer = _mapper.Map<CustomerDTO, Customer>(customerDTO);
            AccountData accountData = new AccountData()
            {
                Customer = customer,
                OpenDate = DateTime.UtcNow,
                Balance = "1000"
            };

            return await _Storage.CreateCustomerAccount(customer, accountData);
        }
        
        public async Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId)
        {
            AccountData accountData = await _Storage.GetAccountData(accountId);
            return _mapper.Map<AccountData,CustomerAccountInfoDTO>(accountData) ;
        }
    }
}
