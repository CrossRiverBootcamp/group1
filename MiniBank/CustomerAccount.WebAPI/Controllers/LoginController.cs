using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
       
        // POST api/<LoginController>
        [HttpPost]
        public Task<int> Post([FromBody] LoginDTO loginDTO)
        {

        }

    }
}
