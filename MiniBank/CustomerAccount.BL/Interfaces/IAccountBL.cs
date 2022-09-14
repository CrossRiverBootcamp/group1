using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface IAccountBL
    {
        Task<bool> CreateCustomerAccount(CustomerDTO customerDTO);
        Task<bool> CustumerAccountExists(Guid accountId);
        Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId);
        Task<bool> HandleCreateAccountRequest(CustomerDTO customerDTO);
        Task<BalancesDTO> MakeBankTransfer(Guid fromAccountId, Guid toAccountId, int amount);
        Task<bool> SenderHasEnoughBalance(Guid accountId, int amount);
        Task UpdateAndLimitNumberOfAttempts(string email);
    }
}