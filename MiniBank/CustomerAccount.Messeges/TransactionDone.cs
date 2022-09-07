using NServiceBus;

namespace CustomerAccount.Messeges
{
    public class TransactionDone : IEvent
    {
        public Guid TransactionId { get; set; }
        public bool IsDone { get; set; }
        public string? FailureReason { get; set; }
    }
}