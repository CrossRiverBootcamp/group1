using CustomerAccount.DTO;
using System.Security.Claims;

namespace CustomerAccount.BL.Interfaces
{
    public interface IAccountBL
    {
        Task<bool> CustomerExists(string email);
        Task<bool> CustumerAccountExists(Guid accountId);
        Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId);
        Task<bool> HandleCreateAccountRequest(CustomerDTO customerDTO);
        Task MakeBankTransferAndSaveOperationsToDB(Guid transactionId,Guid fromAccountId, Guid toAccountId, int amount);
        Task<bool> SenderHasEnoughBalance(Guid accountId, int amount);
        Task<string> GetCustomersEmail(Guid accountId);
        public Guid GetAccountIDFromToken(ClaimsPrincipal User);
      

    }
}