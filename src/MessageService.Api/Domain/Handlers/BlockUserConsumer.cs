using MassTransit;

namespace MessageService.Api
{
    public class BlockUserConsumer : IConsumer<BlockUser>
    {
        readonly ILogger<BlockUserConsumer> _logger;
        private readonly IBlockedUserRepository _blockedUserRepository;

        public BlockUserConsumer(ILogger<BlockUserConsumer> logger, IBlockedUserRepository blockedUserRepository)
        {
            _logger = logger;
            _blockedUserRepository = blockedUserRepository;
        }

        public async Task Consume(ConsumeContext<BlockUser> context)
        {
            var blockedUser = await _blockedUserRepository.GetAsync(context.Message.From, context.Message.To);
            if (blockedUser != null)
            {
                await _blockedUserRepository.RemoveAsync(context.Message.From, context.Message.To);
                _logger.LogInformation($"{context.Message.From} unblocked to {context.Message.To}");
            }
            else
            {
                await _blockedUserRepository.AddAsync(context.Message);
                _logger.LogInformation($"{context.Message.From} blocked to {context.Message.To}");
            }
        }
    }
}