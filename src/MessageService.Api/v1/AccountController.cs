using System.Diagnostics.CodeAnalysis;
using MediatR;
using MessageService.Api.Controllers.UseCases.Account.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<GetUsersCommandResult> GetUsers()
        {
            return await _mediator.Send(new GetUsersCommand());
        }

        [Authorize]
        [HttpGet("activities")]
        public async Task<GetActivitiesCommandResult> GetActivities()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.Items["Email"]?.ToString();
            return await _mediator.Send(new GetActivitiesCommand()
            {
                Email = currentUserEmail
            });
        }

        [Authorize]
        [HttpPost("block-user")]
        public async Task<BlockUserCommandResult> BlockUser([FromBody] BlockUserContract request)
        {
            var currentUser = _httpContextAccessor.HttpContext?.Items["User"]?.ToString();
            return await _mediator.Send(new BlockUserCommand()
            {
                BlockedUser = request.BlockedUser,
                BlockedUserEmail = request.BlockedUserEmail,
                CurrentUser = currentUser
            });
        }

        [HttpPost("sign-up")]
        public async Task<SignUpCommandResult> SignUpAsync([FromBody] SignUpCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("sign-in")]
        public async Task<SignInCommandResult> SignInAsync([FromBody] SignInCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("refresh-token")]
        public async Task<RefreshTokenCommandResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}