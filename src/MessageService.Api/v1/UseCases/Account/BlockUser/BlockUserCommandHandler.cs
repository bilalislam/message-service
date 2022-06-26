using MassTransit;
using MediatR;
using MessageService.Api.Domain.Constants;

namespace MessageService.Api;

public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, BlockUserCommandResult>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IUserRepository _userRepository;

    public BlockUserCommandHandler(ISendEndpointProvider sendEndpointProvider, IUserRepository userRepository)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _userRepository = userRepository;
    }

    public async Task<BlockUserCommandResult> Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        var getUser = await _userRepository.GetByEmailAndNameAsync(request.BlockedUserEmail, request.BlockedUser,
                cancellationToken);
        if (getUser == null)
        {
            return new BlockUserCommandResult()
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