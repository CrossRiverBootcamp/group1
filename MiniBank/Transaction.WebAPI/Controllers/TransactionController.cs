using Microsoft.AspNetCore.Mvc;
using Transaction.BL;
using Transaction.DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transaction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionBL _transactionBL;
        public TransactionController(ITransactionBL transactionBL)
        {
            _transactionBL = transactionBL;
        }

        // POST api/<TransactionController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] TransactionDTO transactionDTO)
        {
            var result = await _transactionBL.PostTransactionStartSaga(transactionDTO);
            return Ok(result);
        }
    }
}
