using MediatR;

namespace MessageService.Api;

public class GetHistoriesCommand : IRequest<GetHistoriesCommandResult>
{
    public string CurrentUser { get; set; }
    public string ReceiverUser { get; set; }
}

public class MessageDto
{
    public string From { get; set; }
    public string To { get; set; }
    public string Message { get; set; }
}