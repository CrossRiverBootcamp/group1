
using CustomerAccount.BL.Interfaces;
using CustomerAccount.Messeges;
using NServiceBus;
using NServiceBus.Logging;

namespace CustomerAccount.WebAPI.NSB.Handlers;


 public class DeleteExpiredRowsHandler :  IHandleMessages<DeleteExpiredRows>
    
         
        {
            private readonly IAccountBL _accountBL;
            static readonly ILog log = LogManager.GetLogger<DeleteExpiredRowsHandler>();

            public DeleteExpiredRowsHandler(IAccountBL accountBL)
            {
                _accountBL = accountBL;
            }
            public async Task Handler(DeleteExpiredRows message, IMessageHandlerContext context)
            {

                try
                {
                    await _accountBL.DeleteExpiredRows();
                    log.Info($"delete expired rows , day = {message.Date}");

                }
                catch (Exception ex)
                {
                    //מה לעשות פה
                }


            }
        }
    

