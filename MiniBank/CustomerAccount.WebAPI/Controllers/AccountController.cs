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
        var result = await accountBL.GetAccountInfo(accountId);
        return Ok(result);
    }

    // POST api/<AccountController>
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] CustomerDTO customerDTO)
    {
        var result = await accountBL.CreateAccount(customerDTO);
        return Ok(result);
    }
}
