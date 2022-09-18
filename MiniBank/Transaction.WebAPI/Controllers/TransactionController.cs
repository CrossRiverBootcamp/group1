using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NServiceBus;
using System.Security.Claims;
using Transaction.BL.Interfaces;
using Transaction.DTO;

namespace Transaction.WebAPI.Controllers
{
    [Authorize]
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
            if (_transactionBL.GetAccountIDFromToken(User).Equals(transactionDTO.FromAccountId))
            {
               var  result = await _transactionBL.PostTransactionStartSaga(transactionDTO, _messageSession);
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}
