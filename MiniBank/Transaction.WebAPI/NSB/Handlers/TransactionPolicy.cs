using CustomerAccount.Messeges;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.DTO;
using Transaction.Messeges;
using ExtendedExceptions;
using Transaction.BL.Interfaces;

namespace Transaction.WebAPI.NSB.Handlers;

public class TransactionPolicy :
    Saga<TransactionPolicyData>,
    IAmStartedByMessages<TransactionReqMade>,
    IHandleMessages<TransactionDone>

{
    private readonly ITransactionBL _transactionBL;
    public TransactionPolicy(ITransactionBL transactionBL)
    {
        _transactionBL = transactionBL;
    }

    static ILog log = LogManager.GetLogger<TransactionPolicy>();

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
    {
        mapper.MapSaga(sagaData => sagaData.TransactionId)
           .ToMessage<TransactionReqMade>(message => message.TransactionId)
           .ToMessage<TransactionDone>(message => message.TransactionId);
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
        
        return context.Send(makeTransfer);
    }
    public async Task Handle(TransactionDone message, IMessageHandlerContext context)
    {
        log.Info($"Transaction request has returned with status: {message.IsDone}. TransactionId: {message.TransactionId}");

        UpadateTransactionStatusDTO upadateTransactionStatusDTO = new UpadateTransactionStatusDTO()
        {
            TransactioId = message.TransactionId,
            IsSuccess = message.IsDone,
            FailureReasun = message.FailureReason
        };

        try
        {
            await _transactionBL.ChangeTransactionStatus(upadateTransactionStatusDTO);
        }
        catch(DBContextException ex)
        {
            log.Info($"Updating transactions status failed with message: {ex.Message}. TransactionId: {message.TransactionId}");
        }

        //If sender accountId currect: informs him.
        if(message.SendersEmail != null)
             _transactionBL.InformCustomerWithTrasactionStatus(message.SendersEmail, message.IsDone);
        //If transaction successed: informs reciever.
        if (message.RecieversEmail != null)
            _transactionBL.InformAccuntCredited(message.RecieversEmail);

        MarkAsComplete();
    }
}
