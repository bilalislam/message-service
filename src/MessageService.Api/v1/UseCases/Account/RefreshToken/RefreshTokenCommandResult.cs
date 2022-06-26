using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

public class RefreshTokenCommandResult : CommandResultBase
{
    public Token Token { get; set; }
}