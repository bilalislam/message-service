using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

public class SignInCommandResult : CommandResultBase
{
    public Token Token { get; set; }
}