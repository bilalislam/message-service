using System.Diagnostics.CodeAnalysis;
using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

[ExcludeFromCodeCoverage]
public class SignInCommandResult : CommandResultBase
{
    public Token Token { get; set; }
}