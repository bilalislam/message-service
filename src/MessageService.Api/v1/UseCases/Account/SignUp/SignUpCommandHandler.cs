using MediatR;
using MessageService.Api.Domain.Constants;

namespace MessageService.Api;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpCommandResult>
{
    private readonly IUserRepository _userRepository;

    public SignUpCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<SignUpCommandResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = User.Load(request);
        var getUserByEmail = await _userRepository.GetAsync(user.Email, cancellationToken);
        if (getUserByEmail != null)
        {
            return new SignUpCommandResult()
            {
                ValidateState = ValidationState.AlreadyExist,
                Messages = new[]
                {
                    new MessageContractDto()
                    {
                        Code = nameof(DomainErrorCodes.EDService1001),
                        Content = DomainErrorCodes.EDService1001,
                        Title = "Error",
                    }
                },
                Success = false,
                ReturnPath = "/sign-up"
            };
        }

        await _userRepository.AddAsync(user, cancellationToken);

        return new SignUpCommandResult()
        {
            Success = true,
            ValidateState = ValidationState.Valid,
            ReturnPath = "/sign-up"
        };
    }
}