using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api.Controllers.UseCases.Account.GetUsers;

[ExcludeFromCodeCoverage]
public class UserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
}