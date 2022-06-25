using MediatR;

namespace MessageService.Api
{
    public class SignInCommand : IRequest<SignInCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInCommandResult
    {
        public Token Token { get; set; }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInCommandResult>
    {

        public Task<SignInCommandResult> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}