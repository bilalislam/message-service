using MassTransit;
using MediatR;

namespace MessageService.Api;

public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, BlockUserCommandResult>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public BlockUserCommandHandler(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<BlockUserCommandResult> Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        var blockUser = BlockUser.Load(request);
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:block-user"));
        await endpoint.Send(blockUser, cancellationToken);
        return new BlockUserCommandResult()
        {
            Success = true,
            ValidateState = ValidationState.Valid,
            ReturnPath = "/block-user"
        };
    }
}