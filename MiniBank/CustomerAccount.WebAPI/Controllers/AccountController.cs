using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL.Interfaces;

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
    public  Task<CustomerAccountInfoDTO> Get(Guid accountId)
    {
        return accountBL.GetAccountInfo(accountId);
    }

    // POST api/<AccountController>
    [HttpPost]
    public Task<bool> Post([FromBody] CustomerAccountDTO customerAccountDTO)
    {
        return  accountBL.CreateAccount(customerAccountDTO);
    }

 
}
