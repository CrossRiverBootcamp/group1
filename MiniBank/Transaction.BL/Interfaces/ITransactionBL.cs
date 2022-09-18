using NServiceBus;
using System.Security.Claims;
using Transaction.DAL.Models;
using Transaction.DTO;

namespace Transaction.BL.Interfaces
{
    public interface ITransactionBL
    {
        Task<bool> PostTransactionStartSaga(TransactionDTO TransactionDTO, IMessageSession _messageSession);
        Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO);
        public Guid getAccountIDFromToken(ClaimsPrincipal User);
        void InformCustomerWithTrasactionStatus(string email, bool isDone);
    }
}