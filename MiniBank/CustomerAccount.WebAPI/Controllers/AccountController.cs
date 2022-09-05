using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;

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
    public Task<AccountDTO> Get(int accountId)
    {
        return IAccountBL.GetAccountinfo(accountId);
    }

    // POST api/<AccountController>
    [HttpPost]
    public Task<bool> Post([FromBody] AccountDTO accountDTO)
    {
        return IAccountBL.CreateAccount(accountDTO);
    }

 
}
