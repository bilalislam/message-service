using MediatR;

namespace MessageService.Api;

public class GetMessagedUsersCommandHandler : IRequestHandler<GetMessageUsersCommand, GetMessageUsersCommandResult>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagedUsersCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<GetMessageUsersCommandResult> Handle(GetMessageUsersCommand request,
        CancellationToken cancellationToken)
    {
        var getMessageUsers = await _messageRepository.GetMessagedUsersAsync(request.CurrentUser, cancellationToken);
        return new GetMessageUsersCommandResult()
        {
            Success = true,
            ReturnPath = "/messages",
            Users = getMessageUsers.Select(x => new MessageUserDto()
            {
                Name = x.To
            }).ToList(),
            ValidateState = ValidationState.Valid
        };
    }
}