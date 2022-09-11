using System.Threading.Tasks;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.Messeges;
using ExtendedExceptions;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Messeges;

namespace CustomerAccount.NSB;

public class MakeTransferHandler :
    IHandleMessages<MakeTransfer>
{
    private readonly IAccountBL _accountBL;
    public MakeTransferHandler(IAccountBL accountBL)
    {
        this._accountBL = accountBL;
    }
    static ILog log = LogManager.GetLogger<MakeTransferHandler>();
  

    /*
o Handle new transaction message 
 
 Check sender balance 
 Update receiver/sender balances (run in DB 
transaction) 
 Send result NSB event 
 Finish saga handler in transaction service         
*/
    public async Task Handle(MakeTransfer message, IMessageHandlerContext context)
    {
        TransactionDone transactionDoneMsg = new TransactionDone() { TransactionId= message.TransactionId };
        //Check correctness of accounts ids 
        try 
        { 
            if (!(await _accountBL.CustumerAccountExists(message.FromAccountId)))
            {
                log.Error($"Transfer failed, TransactionId = {message.TransactionId} - FromAccountId does not exist...");
                transactionDoneMsg.IsDone = false;
                transactionDoneMsg.FailureReason += " Transfer failed, FromAccountId does not exist...";
            }
            else
            {
                if (!(await _accountBL.CustumerAccountExists(message.ToAccountId)))
                {
                    log.Error($"Transfer failed, TransactionId = {message.TransactionId} - ToAccountId does not exist...");
                    transactionDoneMsg.IsDone = false;
                    transactionDoneMsg.FailureReason += " Transfer failed, ToAccountId does not exist...";
                }
                else
                {
                    //Check sender balance 
                    if (!(await _accountBL.SenderHasEnoughBalance(message.ToAccountId,message.Amount)))
                    {
                        log.Error($"Transfer failed, TransactionId = {message.TransactionId} - FromAccountId = {message.FromAccountId} does not Have Enough Balance");
                        transactionDoneMsg.IsDone = false;
                        transactionDoneMsg.FailureReason += " Transfer failed, sender does not Have Enough Balance";
                    }
                    else
                    {
                        //Update receiver/sender balances (run in DB transaction) 
                        await _accountBL.MakeBankTransfer(message.FromAccountId, message.ToAccountId, message.Amount);
                        log.Info($"Transfer succeded, TransactionId = {message.TransactionId} ");
                        transactionDoneMsg.IsDone = true;
                    }
                }
            }
             await context.Publish(transactionDoneMsg);
            return;
        }
        catch (DBContextException error)
        {
            log.Error($"Transfer failed, TransactionId = {message.TransactionId}. Exception: {error.Message}");
        }
        
        
       
    }
}