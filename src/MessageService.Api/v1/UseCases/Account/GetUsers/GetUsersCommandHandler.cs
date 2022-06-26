using MediatR;
using MessageService.Api.Controllers.UseCases.Account.GetUsers;

namespace MessageService.Api;

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, GetUsersCommandResult>
{
    private readonly IUserRepository _userRepository;

    public GetUsersCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUsersCommandResult> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        var getUsers = await _userRepository.ListAsync(cancellationToken);
        if (!getUsers.Any())
        {
            return new GetUsersCommandResult()
            {
                Success = false,
                ReturnPath = "/users",
                ValidateState = ValidationState.DoesNotExist
            };
        }

        return new GetUsersCommandResult()
        {
            Success = true,
            Users = getUsers.Select(x => new UserDto() { Name = x.Name }).ToList(),
            ReturnPath = "/users",
            ValidateState = ValidationState.Valid
        };
    }
}