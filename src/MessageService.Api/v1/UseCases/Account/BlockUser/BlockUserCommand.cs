using MediatR;

namespace MessageService.Api;

public class BlockUserCommand : IRequest<BlockUserCommandResult>
{
    public string CurrentUser { get; set; }
    public string BlockedUser { get; set; }
}