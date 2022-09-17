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
        private readonly IStorage _storage;

        public AccountBL(IMapper mapper, IEmailVerificationBL emailVerificationBL, IStorage Storage)
        {
            _mapper = mapper;
            _emailVerificationBL = emailVerificationBL;
            _storage = Storage;
        }
        private async Task<bool> CreateCustomerAccount(CustomerDTO customerDTO)
        {
            CustomerModel customerModel = _mapper.Map<CustomerDTO, CustomerModel>(customerDTO);
            AccountData accountData = new AccountData()
            {
                OpenDate = DateTime.UtcNow,
                Balance = 100000
            };

            return await _storage.CreateCustomerAccount(customerModel, accountData);
        }
        public async Task<bool> CustomerExists(string email)
        {
            return await _storage.CustomerExists(email);
        }
        public async Task<bool> HandleCreateAccountRequest(CustomerDTO customerDTO)
        {
            //update number of attempts
            await _emailVerificationBL.UpdateAndLimitNumberOfAttempts(customerDTO.Email);

            //verify code 
            bool isAuthorized = await _emailVerificationBL.ValidateCodeAndTime(customerDTO);

            //if not authorized: throw error
            if (!isAuthorized)
                throw new UnauthorizedAccessException();

            //if authorized: create customer account
            return await CreateCustomerAccount(customerDTO);
        }
        public Task<bool> CustumerAccountExists(Guid accountId)
        {
            return _storage.CustumerAccountExists(accountId);
        }
        public async Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId)
        {
            AccountData accountData = await _storage.GetAccountData(accountId);
            return _mapper.Map<AccountData, CustomerAccountInfoDTO>(accountData);
        }
        public async Task MakeBankTransferAndSaveOperationsToDB(Guid transactionId,Guid fromAccountId, Guid toAccountId, int amount)
        {
             await _storage.MakeBankTransferAndSaveOperationsToDB(transactionId,fromAccountId, toAccountId, amount);
        }
        public Task<bool> SenderHasEnoughBalance(Guid accountId, int amount)
        {
            return _storage.SenderHasEnoughBalance(accountId, amount);
        }
    }
}
