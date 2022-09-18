using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface ILoginBL
    {
        string CreateToken(Guid AccountId);
        Task<loginReturnDTO> Login(LoginDTO loginDTO);
    }
}