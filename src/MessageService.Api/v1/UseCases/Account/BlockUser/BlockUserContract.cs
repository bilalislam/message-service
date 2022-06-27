using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api;

[ExcludeFromCodeCoverage]
public class BlockUserContract
{
    public string BlockedUser { get; set; }
    public string BlockedUserEmail { get; set; }
}