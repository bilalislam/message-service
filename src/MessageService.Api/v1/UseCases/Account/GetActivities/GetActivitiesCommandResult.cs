using System.Diagnostics.CodeAnalysis;
using MessageService.Api.Domain.Commands;

namespace MessageService.Api;

[ExcludeFromCodeCoverage]
public class GetActivitiesCommandResult : CommandResultBase
{
    public List<ActivityDto> Activities { get; set; }
}