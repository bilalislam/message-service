using MediatR;

namespace MessageService.Api;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenProxy _tokenProxy;

    public RefreshTokenCommandHandler(IUserRepository userRepository, ITokenProxy tokenProxy)
    {
        _userRepository = userRepository;
        _tokenProxy = tokenProxy;
    }

    public async Task<RefreshTokenCommandResult> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user != null && user.Token.RefreshToken == request.RefreshToken &&
            user.Token.RefreshTokenExpiration.ToLocalTime() > DateTime.Now)
        {
            user.CreateToken(_tokenProxy);
            await _userRepository.UpdateAsync(user, cancellationToken);
            
            return new RefreshTokenCommandResult()
            {
                Token = user.Token,
                Success = true,
                ReturnPath = "/refresh-token",
                ValidateState = ValidationState.Valid
            };
        }

        return new RefreshTokenCommandResult()
        {
            Success = false,
            ReturnPath = "/sign-in",
            ValidateState = ValidationState.DoesNotExist
        };
    }
}