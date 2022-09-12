using Transaction.DTO;

namespace Transaction.DAL
{
    public interface ITransactionDAL
    {
        Task<Guid> PostTransaction(Entities.Transaction Transaction);
        Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO);
    }
}