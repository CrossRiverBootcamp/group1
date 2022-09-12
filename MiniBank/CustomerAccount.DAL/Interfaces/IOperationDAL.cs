using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Models;

namespace CustomerAccount.DAL.Interfaces
{
    public interface IOperationDAL
    {
        Task<IEnumerable<OperationData>> GetByPageAndAccountId(Guid AccountId, SortDirection sortDirection, int PageNumber, int PageSize);
    }
}