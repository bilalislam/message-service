using MessageService.Api.Domain.Commands;

namespace MessageService.Api.Controllers.UseCases.Account.GetUsers;

public class GetUsersCommandResult : CommandResultBase
{
    public List<UserDto> Users { get; set; }
}