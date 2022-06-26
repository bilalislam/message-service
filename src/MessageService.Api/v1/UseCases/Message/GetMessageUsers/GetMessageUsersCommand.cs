using MediatR;

namespace MessageService.Api;

public class GetMessageUsersCommand : IRequest<GetMessageUsersCommandResult>
{
    public string CurrentUser { get; set; }
}