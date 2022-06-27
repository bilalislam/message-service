using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api;

[ExcludeFromCodeCoverage]
public class MessageDto
{
    public string From { get; set; }
    public string To { get; set; }
    public string Message { get; set; }
}