using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace MessageService.Api.Controllers.UseCases.Account.GetUsers;

[ExcludeFromCodeCoverage]
public class GetUsersCommand : IRequest<GetUsersCommandResult>
{
}