using NServiceBus;
using Transaction.DTO;

namespace Transaction.BL.Interfaces
{
    public interface ITransactionBL
    {
        Task<bool> PostTransactionStartSaga(TransactionDTO TransactionDTO, IMessageSession _messageSession);
        Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO);
    }
}