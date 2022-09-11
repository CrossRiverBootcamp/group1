﻿using Microsoft.AspNetCore.Mvc;
using CustomerAccount.DTO;
using CustomerAccount.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationBL _operationBL;

        public OperationController(IOperationBL OperationBL)
        {
            _operationBL = OperationBL;
        }


        // GET api/<OperationController>/5
        [HttpGet("{id}")]
        public Task<List<OperationDTO>> GetByPageAndAccountId([FromQuery] int AccountId, [FromQuery] int PageNumber, [FromQuery] int PageSize)
        {

            return   _operationBL.GetByPageAndAccountId(AccountId, PageNumber, PageSize);
        }


    }
}