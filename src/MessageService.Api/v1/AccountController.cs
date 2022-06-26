using System.Security.Authentication;
using MediatR;
using MessageService.Api.Controllers.UseCases.Account.GetUsers;
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

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("users")]
        public Task<GetUsersCommandResult> GetUsers()
        {
            return _mediator.Send(new GetUsersCommand());
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
        public async Task<SignUpCommandResult> SignUpAsync([FromBody] SignUpCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("sign-in")]
        public async Task<SignInCommandResult> SignInAsync([FromBody] SignInCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("refresh-token")]
        public async Task<RefreshTokenCommandResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}