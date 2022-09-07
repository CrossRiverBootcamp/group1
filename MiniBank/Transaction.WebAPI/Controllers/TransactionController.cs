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
        private readonly ITransactionBL TransactionBL;
        public TransactionController(ITransactionBL TransactionBL)
        {
            this.TransactionBL = TransactionBL;
        }

        // POST api/<TransactionController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] TransactionDTO transactionDTO)
        {
            var result = await TransactionBL.PostTransactionStartSaga(transactionDTO);
            return Ok(result);
        }
    }
}
