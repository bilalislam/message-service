using System.Text.Json.Serialization;
using MediatR;

namespace MessageService.Api;

public class BlockUserCommand : IRequest<BlockUserCommandResult>
{
    [JsonIgnore] public string CurrentUser { get; set; }
    public string BlockedUser { get; set; }
    public string BlockedUserEmail { get; set; }
}