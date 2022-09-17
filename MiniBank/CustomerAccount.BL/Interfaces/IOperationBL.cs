using CustomerAccount.DAL.Entities;
using CustomerAccount.DTO;
using Transaction.Messeges;

namespace CustomerAccount.BL.Interfaces
{
    public interface IOperationBL
    {
        Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountIdion, int PageNumber, int PageSize);
        Task<TransactionPartnerDetailsDTO> GetTransactionPartnerAccountInfo(Guid transactionPartnerAccountId);
    }
}