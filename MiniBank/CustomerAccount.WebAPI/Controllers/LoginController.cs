using CustomerAccount.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<int> Post([FromBody] LoginDTO loginDTO)
        {
           return await loginBL.Login(loginDTO);
        }

    }
}
