using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

public class GetMessageUsersCommandResult : CommandResultBase
{
    public List<string> Users { get; set; }
}