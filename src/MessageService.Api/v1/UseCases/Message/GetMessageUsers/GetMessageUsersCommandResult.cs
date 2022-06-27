using System.Diagnostics.CodeAnalysis;
using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

[ExcludeFromCodeCoverage]
public class GetMessageUsersCommandResult : CommandResultBase
{
    public List<MessageUserDto> Users { get; set; }
}