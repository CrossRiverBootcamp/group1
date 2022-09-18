namespace Transaction.Messeges
{
    public interface IPayloadMesseges
    {
        public Guid TransactionId { get; set; }
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public int Amount { get; set; }

    }
}