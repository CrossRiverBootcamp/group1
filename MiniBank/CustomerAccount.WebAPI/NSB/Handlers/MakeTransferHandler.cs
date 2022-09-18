using CustomerAccount.BL;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.Messeges;
using ExtendedExceptions;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Messeges;

namespace CustomerAccount.WebAPI.NSB.Handlers;

public class MakeTransferHandler :
    IHandleMessages<MakeTransfer>
{
    private readonly IAccountBL _accountBL;

    public MakeTransferHandler(IAccountBL accountBL)
    {
        _accountBL = accountBL;
    }
    static ILog log = LogManager.GetLogger<MakeTransferHandler>();
  
    public async Task Handle(MakeTransfer message, IMessageHandlerContext context)
    {
        TransactionDone transactionDoneMsg = new TransactionDone() { TransactionId= message.TransactionId };
        //Check currectness of accounts ids 
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
                //If he exists: get senders email for informing
                transactionDoneMsg.SendersEmail = await _accountBL.GetCustomersEmail(message.FromAccountId);

                if (!(await _accountBL.CustumerAccountExists(message.ToAccountId)))
                {
                    log.Error($"Transfer failed, TransactionId = {message.TransactionId} - ToAccountId does not exist...");
                    transactionDoneMsg.IsDone = false;
                    transactionDoneMsg.FailureReason += " Transfer failed, ToAccountId does not exist...";
                }
                else
                {
                    //Check sender balance 
                    if (!(await _accountBL.SenderHasEnoughBalance(message.FromAccountId,message.Amount)))
                    {
                        log.Error($"Transfer failed, TransactionId = {message.TransactionId} - FromAccountId = {message.FromAccountId} does not Have Enough Balance");
                        transactionDoneMsg.IsDone = false;
                        transactionDoneMsg.FailureReason += " Transfer failed, sender does not Have Enough Balance";
                    }
                    else
                    {
                        //Update receiver/sender balances (run in DB transaction) 
                        await _accountBL.MakeBankTransferAndSaveOperationsToDB(message.TransactionId,message.FromAccountId, message.ToAccountId, message.Amount);
                        log.Info($"Transfer succeded, TransactionId = {message.TransactionId} ");
                        transactionDoneMsg.IsDone = true;

                        //Success: Get recievers email for informing
                        transactionDoneMsg.RecieversEmail = await _accountBL.GetCustomersEmail(message.ToAccountId);  
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