using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL.Models;

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
        [HttpGet("{id}")]
        public Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId, [FromQuery] SortDirection sortDirection, [FromQuery] int PageNumber, [FromQuery] int PageSize)
        {

            return _operationBL.GetByPageAndAccountId(AccountId, sortDirection, PageNumber, PageSize);
        }


    }
}
