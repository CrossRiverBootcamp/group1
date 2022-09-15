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
        public void Post([FromBody] string email)
        {
            _emailVerificationBL.HandleEmailVerificationRequest(email);
        }
        [HttpPost("ResendEmail")]
        public void PostResendEmailVerification([FromBody] string email)
        {
            _emailVerificationBL.HandleEmailVerificationRequest(email);
        }
    }
}
