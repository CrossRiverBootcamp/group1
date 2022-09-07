using Transaction.DTO;

namespace Transaction.DAL
{
    public interface ITransactionDal
    {
        Task<Guid> PostTransaction(Entities.Transaction Transaction);
        Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO);
    }
}