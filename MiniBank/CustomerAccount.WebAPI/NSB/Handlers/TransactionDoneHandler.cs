using System.Threading.Tasks;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.Messeges;
using ExtendedExceptions;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Messeges;

namespace CustomerAccount.WebAPI.NSB.Handlers;

public class TransactionDoneHandler :
    IHandleMessages<TransactionDone>
{
    private readonly IAccountBL _accountBL;
    public TransactionDoneHandler(IAccountBL accountBL)
    {
        this._accountBL = accountBL;
    }
    static ILog log = LogManager.GetLogger<TransactionDoneHandler>();
  
    public async Task Handle(TransactionDone message, IMessageHandlerContext context)
    {
        try
        {
            // write operations to DB

        }
        catch (DBContextException error)
        {
            log.Error($"Transfer failed, TransactionId = {message.TransactionId}. Exception: {error.Message}");
        }

    }
}