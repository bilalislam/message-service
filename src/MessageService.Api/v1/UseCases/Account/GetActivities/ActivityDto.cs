using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api;

[ExcludeFromCodeCoverage]
public class ActivityDto
{
    public string Email { get; set; }
    public string Event { get; set; }
    public DateTime EventOn { get; set; }
}