
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {

        public AccountController()
        {

        }

        [Authorize]
        [HttpGet("users")]
        public ActionResult GetUsers()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("activities")]
        public ActionResult GetActivities()
        {

            return Ok();
        }

        //blocked user consumer ve blocked users tablosu ac rediste
        // validate user,olan bir user'ı block edebilir
        [Authorize]
        [HttpPost("block-user")]
        public ActionResult BlockUser()
        {

            return Ok();

        }

        [Authorize]
        [HttpPut("unblock-user")]
        public ActionResult UnBlockUser()
        {
            return Ok();
        }


        ///todo : create user consumer
        [HttpPost("sign-up")]
        public ActionResult SignUp()
        {

            return Ok();
        }

        /// sync calısması gerekli,gecikme olursa login olamaz.
        /// basarılı veya basarısız loginler activityconsumer'a at
        [HttpPost("sign-in")]
        public ActionResult SignIn()
        {
            return Ok();
        }

        /// sync calısması lazım
        [HttpGet("refresh-token")]
        public ActionResult RefreshToken()
        {
            return Ok();
        }
    }
}