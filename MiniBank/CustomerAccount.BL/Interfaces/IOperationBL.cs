using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface IOperationBL
    {
        Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId, int PageNumber, int PageSize);
    }
}