using System.Security.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MessageService.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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

        [HttpPost("sign-up")]
        public async Task<SignUpCommandResult> SignUp([FromBody] SignUpCommand request)
        {
            return await _mediator.Send(request);
        }

        /// sync calısması gerekli,gecikme olursa login olamaz.
        /// basarılı veya basarısız loginler activityconsumer'a at
        [HttpPost("sign-in")]
        public async Task<SignInCommandResult> SignInAsync([FromBody] SignInCommand request)
        {
            return await _mediator.Send(request);
        }

        /// sync calısması lazım
        [HttpGet("refresh-token")]
        public ActionResult RefreshToken()
        {
            return Ok();
        }
    }
}