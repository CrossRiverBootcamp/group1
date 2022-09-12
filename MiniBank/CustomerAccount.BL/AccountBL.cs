using AutoMapper;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Interfaces;
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
        private readonly ICustomerAccountDAL _CustomerAccountDAL;
        public AccountBL(IMapper mapper, ICustomerAccountDAL CustomerAccountDAL)
        {
            _mapper = mapper;
            _CustomerAccountDAL = CustomerAccountDAL;
        }
        public async Task<bool> CreateAccount(CustomerDTO customerDTO)
        {
            bool isExists = await _CustomerAccountDAL.CustomerExists(customerDTO.Email);
            if (isExists)
                return false;

            Customer customer = _mapper.Map<CustomerDTO, Customer>(customerDTO);
            AccountData accountData = new AccountData()
            {
                Customer = customer,
                OpenDate = DateTime.UtcNow,
                Balance = 100000
            };

            return await _CustomerAccountDAL.CreateCustomerAccount(customer, accountData);
        }

        public Task<bool> CustumerAccountExists(Guid accountId)
        {
           return _CustomerAccountDAL.CustumerAccountExists(accountId);

        }

        public async Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId)
        {
            AccountData accountData = await _CustomerAccountDAL.GetAccountData(accountId);
            return _mapper.Map<AccountData,CustomerAccountInfoDTO>(accountData) ;
        }

        public Task MakeBankTransfer(Guid fromAccountId, Guid toAccountId, int amount)
        {
            return _CustomerAccountDAL.MakeBankTransfer(fromAccountId, toAccountId, amount);
        }

        public Task<bool> SenderHasEnoughBalance(Guid accountId, int amount)
        {
            return _CustomerAccountDAL.SenderHasEnoughBalance(accountId, amount);
        }
    }
}
