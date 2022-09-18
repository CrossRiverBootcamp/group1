using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NServiceBus;
using System.Security.Claims;
using Transaction.BL.Interfaces;
using Transaction.DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transaction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionBL _transactionBL;
        IMessageSession _messageSession;
        public TransactionController(ITransactionBL transactionBL, IMessageSession messageSession)
        {
            _messageSession = messageSession;
            _transactionBL = transactionBL;
        }

        // POST api/<TransactionController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> Post([FromBody] TransactionDTO transactionDTO)
        {

             //bool result = false;
             Guid id = _transactionBL.getAccountIDFromToken(User);
            if (id.Equals(transactionDTO.FromAccountId))
            {
               var  result = await _transactionBL.PostTransactionStartSaga(transactionDTO, _messageSession);
                return Ok(result);
            }
            return Unauthorized();
        }
    
    }
}
