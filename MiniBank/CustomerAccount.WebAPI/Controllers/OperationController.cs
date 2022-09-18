using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.BL;


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
        [HttpGet("{AccountId}/getOperations")]
        public Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId, [FromQuery] int PageNumber, [FromQuery] int PageSize)
        {
            return _operationBL.GetByPageAndAccountId(AccountId, PageNumber, PageSize);
        }

        // GET api/<AccountController>/5
        [HttpGet("{transactionPartnerAccountId}/transactionPartnerAccountId")]
        public async Task<ActionResult<TransactionPartnerDetailsDTO>> GetTransactionPartner(Guid transactionPartnerAccountId)
        {
            return Ok(await _operationBL.GetTransactionPartnerAccountInfo(transactionPartnerAccountId));
        }

        [HttpGet("{AccountId}/getCountOperations")]
        public async Task<ActionResult<int>> GetCountOprations(Guid AccountId)
        {
            return Ok(await _operationBL.GetCountOperations(AccountId));
        }
    }
}
