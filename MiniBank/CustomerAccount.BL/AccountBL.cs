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
        private static int NumOfAttemptsAllowed = 3;
        private static int NumOfVerficationCodesAllowed = 2;

        private readonly IMapper _mapper;
        private readonly ICustomerAccountDAL _CustomerAccountDAL;

        public AccountBL(IMapper mapper, ICustomerAccountDAL CustomerAccountDAL)
        {
            _mapper = mapper;
            _CustomerAccountDAL = CustomerAccountDAL;
        }
        public async Task<bool> HandleCreateAccountRequest(CustomerDTO customerDTO)
        {
            //check if email is in use
            bool isExists = await _CustomerAccountDAL.CustomerExists(customerDTO.Email);
            if (isExists)
                throw new EmailInUseException();

            //verify code 
            bool isAuthorized = await _CustomerAccountDAL.ValidateCodeAndTime(customerDTO.Email, customerDTO.VerificationCode);

            //if not authorized: update number of attempts, throw error
            if (!isAuthorized)
            {
                await UpdateAndLimitNumberOfAttempts(customerDTO.Email);
                throw new UnauthorizedAccessException();
            }

            //if authorized: create customer account
            return await CreateCustomerAccount(customerDTO);
        }

        public async Task<bool> CreateCustomerAccount(CustomerDTO customerDTO)
        {
            CustomerModel customerModel = _mapper.Map<CustomerDTO, CustomerModel>(customerDTO);
            AccountData accountData = new AccountData()
            {
                OpenDate = DateTime.UtcNow,
                Balance = 100000
            };

            return await _CustomerAccountDAL.CreateCustomerAccount(customerModel, accountData);
        }

        //פה או emailverification controller???
        public async Task UpdateAndLimitNumberOfAttempts(string email)
        {
            int numOtAttempts = (await _CustomerAccountDAL.GetNumOfAttempts(email)) + 1;
            if (numOtAttempts == NumOfAttemptsAllowed)
                throw new TooManyRetriesException();

            await _CustomerAccountDAL.UpdateNumOfAttempts(email);
        }

        public Task<bool> CustumerAccountExists(Guid accountId)
        {
            return _CustomerAccountDAL.CustumerAccountExists(accountId);
        }

        public async Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId)
        {
            AccountData accountData = await _CustomerAccountDAL.GetAccountData(accountId);
            return _mapper.Map<AccountData, CustomerAccountInfoDTO>(accountData);
        }
        //moved to operation BL

        //public async Task<TransactionPartnerDetailsDTO> GetTransactionPartnerAccountInfo(Guid transactionPartnerAccountId)
        //{
        //    return _mapper.Map<AccountData, TransactionPartnerDetailsDTO>(await _CustomerAccountDAL.GetAccountData(transactionPartnerAccountId));
        //}
        public async Task<BalancesDTO> MakeBankTransfer(Guid fromAccountId, Guid toAccountId, int amount)
        {
            return _mapper.Map<BalancesDTO>(await _CustomerAccountDAL.MakeBankTransfer(fromAccountId, toAccountId, amount));
        }

        public Task<bool> SenderHasEnoughBalance(Guid accountId, int amount)
        {
            return _CustomerAccountDAL.SenderHasEnoughBalance(accountId, amount);
        }
    }
}
