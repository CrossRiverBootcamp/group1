using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL.Interfaces;

namespace CustomerAccount.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountBL accountBL;

    public AccountController(IAccountBL accountBL)
    {
        this.accountBL = accountBL;
    }

    // GET api/<AccountController>/5
    [HttpGet("{accountId}")]
    public async Task<ActionResult<CustomerAccountInfoDTO>> Get(Guid accountId)
    {
        return Ok(await accountBL.GetAccountInfo(accountId));
    }

    // GET api/<AccountController>/5
    [HttpGet("{transactionPartnerAccountId}")]
    public async Task<ActionResult<TransactionPartnerDetailsDTO>> GetTransactionPartner(Guid transactionPartnerAccountId)
    {
        return Ok(await accountBL.GetTransactionPartnerAccountInfo(transactionPartnerAccountId));
    }

    // POST api/<AccountController>
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] CustomerDTO customerDTO)
    {
        return Ok(await accountBL.CreateAccount(customerDTO));
    }
}
