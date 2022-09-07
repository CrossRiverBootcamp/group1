using CustomerAccount.Messeges;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Messeges;

namespace Transaction.NSB
{
    public class TransactionPolicy :
        Saga<TransactionPolicyData>,
        IAmStartedByMessages<TransactionReqMade>,
        IHandleMessages<TransactionDone>

    {
        static ILog log = LogManager.GetLogger<TransactionPolicy>();
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionId)
               .ToMessage<TransactionReqMade>(message => message.TransactionId)
               .ToMessage<TransactionDone>(message => message.TransactionId)
        }

        public Task Handle(TransactionReqMade message, IMessageHandlerContext context)
        {
            log.Info($"Transaction request made. TransactionId: {message.TransactionId}");

            MakeTransfer makeTransfer = new MakeTransfer()
            {
                TransactionId = message.TransactionId,
                FromAccountId = message.FromAccountId,
                ToAccountId = message.ToAccountId,
                Amount = message.Amount
            };

            return context.Publish(makeTransfer);
        }
        public Task Handle(TransactionDone message, IMessageHandlerContext context)
        {
            log.Info($"Transaction request has returned with status: {message.IsDone}. TransactionId: {message.TransactionId}");
            
            MarkAsComplete();
        }

    }
}
