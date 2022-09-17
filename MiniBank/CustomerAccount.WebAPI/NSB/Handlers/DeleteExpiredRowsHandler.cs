
using CustomerAccount.BL.Interfaces;
using CustomerAccount.Messeges;
using ExtendedExceptions;
using NServiceBus;
using NServiceBus.Logging;
namespace CustomerAccount.WebAPI.NSB.Handlers
{
   
        public class DeleteExpiredRowsHandler :
            IHandleMessages<DeleteExpiredRows>
        {
            private readonly IEmailVerificationBL _emailverificationBL;
            static ILog log = LogManager.GetLogger<DeleteExpiredRowsHandler>();

            public DeleteExpiredRowsHandler(IEmailVerificationBL emailverificationBL)
            {
                _emailverificationBL = emailverificationBL;
            }
            public async Task Handle(DeleteExpiredRows message, IMessageHandlerContext context)
            {
                try
                {
                    await _emailverificationBL.DeleteExpiredRows();
                    log.Info($"delete expired rows succsesfuly, Date: {message.Date}");
                }
                catch (DBContextException ex)
                {
                    log.Info($"delete expired failed with message: {ex.Message}, Date: {message.Date}");
                }
             }
        }
}
