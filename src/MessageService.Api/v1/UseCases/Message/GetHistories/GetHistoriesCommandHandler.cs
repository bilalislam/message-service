using MediatR;

namespace MessageService.Api;

public class GetHistoriesCommandHandler : IRequestHandler<GetHistoriesCommand, GetHistoriesCommandResult>
{
    private readonly IMessageRepository _messageRepository;

    public GetHistoriesCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<GetHistoriesCommandResult> Handle(GetHistoriesCommand request,
        CancellationToken cancellationToken)
    {
        var getHistories = await _messageRepository
            .GetHistoriesAsync(request.CurrentUser, request.ReceiverUser, cancellationToken);

        return new GetHistoriesCommandResult()
        {
            Messages = getHistories.Select(x => new MessageDto()
            {
                To = x.To,
                From = x.From,
                Message = x.Msg
            }).ToList(),
            Success = true,
            ReturnPath = $"/{request.ReceiverUser}/histories",
            ValidateState = ValidationState.Valid
        };
    }
}