using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MessageController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("send")]
        public async Task<SendCommandResult> Send([FromBody] SendCommandContract request)
        {
            var currentUser = _httpContextAccessor.HttpContext?.Items["User"]?.ToString();
            return await _mediator.Send(new SendCommand()
            {
                Sender = currentUser,
                Receiver = request.Receiver,
                ReceiverEmail = request.ReceiverEmail,
                Message = request.Message
            });
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