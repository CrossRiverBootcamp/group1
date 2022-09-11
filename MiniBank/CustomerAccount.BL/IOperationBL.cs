namespace CustomerAccount.BL
{
    public interface IOperationBL
    {
        Task<List<OperationDTO>> GetByPageAndAccountId(int AccountId, int PageNumber, int PageSize);
    }
}