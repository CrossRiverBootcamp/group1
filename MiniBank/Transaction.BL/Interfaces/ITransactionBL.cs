using Transaction.DTO;

namespace Transaction.BL.Interfaces
{
    public interface ITransactionBL
    {
        Task<bool> PostTransactionStartSaga(TransactionDTO TransactionDTO);
        Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO);
    }
}