using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

public class GetActivitiesCommandResult : CommandResultBase
{
    public List<ActivityDto> Activities { get; set; }
}