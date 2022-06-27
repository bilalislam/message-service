using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace MessageService.Api;

[ExcludeFromCodeCoverage]
public class GetHistoriesCommand : IRequest<GetHistoriesCommandResult>
{
    public string CurrentUser { get; set; }
    public string ReceiverUser { get; set; }
}