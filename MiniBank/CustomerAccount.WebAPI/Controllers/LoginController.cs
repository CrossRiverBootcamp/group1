using CustomerAccount.BL.Interfaces;
using CustomerAccount.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBL loginBL;

        public LoginController(ILoginBL loginBL)
        {
            this.loginBL = loginBL;
        }
       
        // POST api/<LoginController>
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] LoginDTO loginDTO)
        {
            var result = await loginBL.Login(loginDTO);
            return Ok(result);
        }
    }
}
