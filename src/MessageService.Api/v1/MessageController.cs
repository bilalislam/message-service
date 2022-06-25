using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        public MessageController()
        {

        }

        ///eger block'lu ise message atma
        /// message consumer ile redis'e kaydet
        [HttpPost("send")]
        public async Task<IActionResult> Send()
        {
            return Accepted();
        }


        [HttpGet("receive")]
        public ActionResult Receive()
        {
            throw new BusinessException("123", "sadsa");
            return Ok();
        }

        [HttpGet("histories")]
        public ActionResult GetHistory()
        {
            return Ok();
        }
    }
}