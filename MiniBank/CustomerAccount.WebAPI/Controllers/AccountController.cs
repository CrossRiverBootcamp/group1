
﻿using Microsoft.AspNetCore.Mvc;
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
    [HttpGet("{email}/Exists")]
    public async Task<ActionResult<bool>> Get(string email)
    {
        return Ok(await accountBL.CustomerExists(email));
    }

    // POST api/<AccountController>
    [HttpPost]
    public async Task<bool> Post([FromBody] CustomerDTO customerDTO)
    {
        return await accountBL.HandleCreateAccountRequest(customerDTO);
        throw new Exception("888");
    }
}
