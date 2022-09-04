using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface IAccountBL
    {
        Task<bool> CreateAccount(CustomerAccountDTO customerAccountDTO);
         Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId);
    }
}