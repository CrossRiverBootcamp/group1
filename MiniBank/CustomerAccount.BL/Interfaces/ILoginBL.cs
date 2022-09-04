using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface ILoginBL
    {
        Task<Guid> Login(LoginDTO loginDTO);
    }
}