using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

public class GetHistoriesCommandResult : CommandResultBase
{
    public List<MessageDto> Messages { get; set; }
}