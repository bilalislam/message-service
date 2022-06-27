using System.Diagnostics.CodeAnalysis;
using MessageService.Api.Domain.Commands;

namespace MessageService.Api.Controllers.UseCases.Account.GetUsers;

[ExcludeFromCodeCoverage]
public class GetUsersCommandResult : CommandResultBase
{
    public List<UserDto> Users { get; set; }
}