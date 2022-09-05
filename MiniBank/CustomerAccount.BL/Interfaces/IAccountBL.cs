using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface IAccountBL
    {
        Task<bool> CreateAccount(CustomerDTO customerDTO);
         Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId);
    }
}