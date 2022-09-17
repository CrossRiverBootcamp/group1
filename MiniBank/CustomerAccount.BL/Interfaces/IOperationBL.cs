using CustomerAccount.DAL.Entities;
using CustomerAccount.DTO;
using Transaction.Messeges;

namespace CustomerAccount.BL.Interfaces
{
    public interface IOperationBL
    {
        Task<List<OperationDTO>> GetByPageAndAccountId(Guid AccountIdion, int PageNumber, int PageSize);
        Task<TransactionPartnerDetailsDTO> GetTransactionPartnerAccountInfo(Guid transactionPartnerAccountId);
    }
}