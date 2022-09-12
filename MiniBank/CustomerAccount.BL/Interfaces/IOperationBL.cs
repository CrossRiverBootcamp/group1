using CustomerAccount.DAL.Models;
using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface IOperationBL
    {
        Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId,SortDirection sortDirection, int PageNumber, int PageSize);
    }
}