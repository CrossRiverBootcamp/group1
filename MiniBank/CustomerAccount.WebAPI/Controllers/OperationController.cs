using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CustomerAccount.WebAPI.Controllers
{
    [Authorize]
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
        [HttpGet("{transactionPartnerAccountId}")]
        public async Task<ActionResult<TransactionPartnerDetailsDTO>> GetTransactionPartner(Guid transactionPartnerAccountId)
        {
            return Ok(await _operationBL.GetTransactionPartnerAccountInfo(transactionPartnerAccountId));
        }
    }
}
