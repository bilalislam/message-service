using System.Diagnostics.CodeAnalysis;
using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

[ExcludeFromCodeCoverage]
public class GetHistoriesCommandResult : CommandResultBase
{
    public List<MessageDto> Messages { get; set; }
}