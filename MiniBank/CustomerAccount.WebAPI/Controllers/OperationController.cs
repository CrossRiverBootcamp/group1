using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationBL _operationBL;

        public OperationController(IOperationBL operationBL)
        {
            _operationBL = operationBL;
        }

        // GET api/<OperationController>/5
        [HttpGet("{AccountId}")]
        public Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId, [FromQuery] int PageNumber, [FromQuery] int PageSize)
        {
            return _operationBL.GetByPageAndAccountId(AccountId, PageNumber, PageSize);
        }

        // GET api/<AccountController>/5
        [HttpGet("{transactionPartnerAccountId}")]
        public async Task<ActionResult<TransactionPartnerDetailsDTO>> GetTransactionPartner(Guid transactionPartnerAccountId)
        {
            return Ok(await _operationBL.GetTransactionPartnerAccountInfo(transactionPartnerAccountId));
        }
    }
}
