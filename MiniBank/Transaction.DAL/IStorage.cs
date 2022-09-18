using Transaction.DTO;

namespace Transaction.DAL
{
    public interface IStorage
    {
        Task<Guid> PostTransaction(Entities.Transaction Transaction);
        Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO);
    }
}