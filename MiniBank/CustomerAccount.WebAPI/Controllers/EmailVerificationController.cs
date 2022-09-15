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
        public async Task<bool> Post([FromBody] string email, bool isResendRequest)
        {
            //catch here exception???????
            try
            {
                await _emailVerificationBL.HandleEmailVerificationRequest(email, isResendRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
