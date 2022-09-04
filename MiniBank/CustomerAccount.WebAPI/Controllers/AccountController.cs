using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        return await IAccountBL.GetAccountInfo(accountId);
    }

    // POST api/<AccountController>
    [HttpPost]
    public async Task<bool> Post([FromBody] AccountDTO accountDTO)
    {
        return await IAccountBL.CreateAccount(accountDTO);
    }

 
}
