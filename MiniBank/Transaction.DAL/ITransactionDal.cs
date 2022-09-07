using Transaction.DTO;

namespace Transaction.DAL
{
    public interface ITransactionDal
    {
        Task<bool> PostTransaction(Entities.Transaction Transaction);
        Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO);
    }
}