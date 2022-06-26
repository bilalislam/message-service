using MediatR;

namespace MessageService.Api.Controllers.UseCases.Account.GetUsers;

public class GetUsersCommand : IRequest<GetUsersCommandResult>
{
}