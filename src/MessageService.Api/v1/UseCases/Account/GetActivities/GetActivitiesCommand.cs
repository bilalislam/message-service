using MediatR;

namespace MessageService.Api;

public class GetActivitiesCommand : IRequest<GetActivitiesCommandResult>
{
    public string Email { get; set; }
}