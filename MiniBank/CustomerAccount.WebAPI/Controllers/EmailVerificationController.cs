using CustomerAccount.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly IEmailVerificationBL _emailVerificationBL;
        public EmailVerificationController(IEmailVerificationBL emailVerificationBL)
        {
            _emailVerificationBL = emailVerificationBL;
        }

        //POST api/<EmailVerificationController>
        [HttpPost]
        public async Task Post([FromBody] string email,[FromQuery] bool isResendRequest)
        {
            await _emailVerificationBL.HandleEmailVerificationRequest(email, isResendRequest);
        }
      
    }
}
