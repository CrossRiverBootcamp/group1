
﻿using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CustomerAccount.WebAPI.Controllers;
[Authorize]
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
    public async Task<CustomerAccountInfoDTO> Get(Guid accountId)
    {
        if (accountBL.getAccountIDFromToken(User).Equals(accountId))
            return await accountBL.GetAccountInfo(accountId);
        throw new UnauthorizedAccessException();

    }
    [HttpGet("{email}/Exists")]
    public async Task<bool> Get(string email)
    {
        return await accountBL.CustomerExists(email);
    }


    [HttpGet("{AccountId}/Exists")]
    public async Task<bool> CustumrAccountExists(Guid AccountId)
    {
        return await accountBL.CustumerAccountExists(AccountId);
    }

    // POST api/<AccountController>
    [AllowAnonymous]
    [HttpPost]
    public async Task<bool> Post([FromBody] CustomerDTO customerDTO)
    {
        return await accountBL.HandleCreateAccountRequest(customerDTO);
    }
}
