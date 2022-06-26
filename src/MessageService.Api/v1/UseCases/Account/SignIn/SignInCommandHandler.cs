using MediatR;

namespace MessageService.Api;

public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInCommandResult>
{
    public Task<SignInCommandResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}