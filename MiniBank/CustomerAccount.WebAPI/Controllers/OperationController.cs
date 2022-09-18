using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CustomerAccount.BL;

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
            if (_operationBL.GetAccountIDFromToken(User).Equals(AccountId))
                return _operationBL.GetByPageAndAccountId(AccountId, PageNumber, PageSize);
            throw new UnauthorizedAccessException();
           
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
            if (_operationBL.GetAccountIDFromToken(User).Equals(AccountId))
               return Ok(await _operationBL.GetCountOperations(AccountId));
            return Unauthorized();
        }
    }
}
