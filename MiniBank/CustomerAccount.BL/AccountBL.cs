using AutoMapper;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.DAL.Models;
using CustomerAccount.DTO;
using ExtendedExceptions;


namespace CustomerAccount.BL
{
    public class AccountBL : IAccountBL
    {
        private readonly IMapper _mapper;
        private readonly IEmailVerificationBL _emailVerificationBL;
        private readonly ICustomerAccountDAL _customerAccountDAL;

        public AccountBL(IMapper mapper, IEmailVerificationBL emailVerificationBL, ICustomerAccountDAL CustomerAccountDAL)
        {
            _mapper = mapper;
            _emailVerificationBL = emailVerificationBL;
            _customerAccountDAL = CustomerAccountDAL;
        }
        private async Task<bool> CreateCustomerAccount(CustomerDTO customerDTO)
        {
            CustomerModel customerModel = _mapper.Map<CustomerDTO, CustomerModel>(customerDTO);
            AccountData accountData = new AccountData()
            {
                OpenDate = DateTime.UtcNow,
                Balance = 100000
            };

            return await _customerAccountDAL.CreateCustomerAccount(customerModel, accountData);
        }
        public async Task<bool> CustomerExists(string email)
        {
            return await _customerAccountDAL.CustomerExists(email);
        }
        public async Task<bool> HandleCreateAccountRequest(CustomerDTO customerDTO)
        {
            //verify code 
            bool isAuthorized = await _emailVerificationBL.ValidateCodeAndTime(customerDTO);

            //if not authorized: update number of attempts, throw error
            if (!isAuthorized)
            {
                await _emailVerificationBL.UpdateAndLimitNumberOfAttempts(customerDTO.Email);
                throw new UnauthorizedAccessException();
            }

            //if authorized: create customer account
            return await CreateCustomerAccount(customerDTO);
        }
        public Task<bool> CustumerAccountExists(Guid accountId)
        {
            return _customerAccountDAL.CustumerAccountExists(accountId);
        }
        public async Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId)
        {
            AccountData accountData = await _customerAccountDAL.GetAccountData(accountId);
            return _mapper.Map<AccountData, CustomerAccountInfoDTO>(accountData);
        }
        public async Task<BalancesDTO> MakeBankTransfer(Guid fromAccountId, Guid toAccountId, int amount)
        {
            return _mapper.Map<BalancesDTO>(await _customerAccountDAL.MakeBankTransfer(fromAccountId, toAccountId, amount));
        }
        public Task<bool> SenderHasEnoughBalance(Guid accountId, int amount)
        {
            return _customerAccountDAL.SenderHasEnoughBalance(accountId, amount);
        }
    }
}
