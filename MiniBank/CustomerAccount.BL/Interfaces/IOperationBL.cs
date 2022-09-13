using CustomerAccount.DTO;
using Transaction.Messeges;

namespace CustomerAccount.BL.Interfaces
{
    public interface IOperationBL
    {
        Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId,SortDirection sortDirection, int PageNumber, int PageSize);
        public Task PostOperations(MakeTransfer makeTransferMsg);
    }
}