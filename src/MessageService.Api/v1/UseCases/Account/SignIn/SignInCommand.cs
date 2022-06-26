using MediatR;

namespace MessageService.Api
{
    public class SignInCommand : IRequest<SignInCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}