using MediatR;

namespace MessageService.Api;

public class RefreshTokenCommand : IRequest<RefreshTokenCommandResult>
{
    public string Email { get; set; }
    public string RefreshToken { get; set; }
}