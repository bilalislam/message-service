using MassTransit;
using MediatR;
using MessageService.Api.Domain.Constants;

namespace MessageService.Api;

public class SendCommandHandler : IRequestHandler<SendCommand, SendCommandResult>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IUserRepository _userRepository;

    public SendCommandHandler(ISendEndpointProvider sendEndpointProvider, IUserRepository userRepository)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _userRepository = userRepository;
    }

    public async Task<SendCommandResult> Handle(SendCommand request, CancellationToken cancellationToken)
    {
        var getReceiverUser =
            await _userRepository.GetByEmailAndNameAsync(request.ReceiverEmail, request.Receiver, cancellationToken);

        if (getReceiverUser == null)
        {
            return new SendCommandResult()
            {
                Messages = new[]
                {
                    new MessageContractDto()
                    {
                        Code = nameof(DomainErrorCodes.EDService1008),
                        Content = DomainErrorCodes.EDService1008,
                        Title = "Error",
                    }
                },
                Success = false,
                ReturnPath = "/users",
                ValidateState = ValidationState.UnProcessable
            };
        }
        
        var msg = Message.Load(request);
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:message"));
        await endpoint.Send(msg, cancellationToken);
        return new SendCommandResult()
        {
            Success = true,
            ValidateState = ValidationState.Valid,
            ReturnPath = "/send"
        };
    }
}