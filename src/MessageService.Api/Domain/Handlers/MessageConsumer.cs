using MassTransit;

namespace MessageService.Api
{
    public class MessageConsumer :
        IConsumer<Message>
    {
        readonly ILogger<MessageConsumer> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IBlockedUserRepository _blockedUserRepository;

        public MessageConsumer(ILogger<MessageConsumer> logger, IMessageRepository messageRepository,
            IBlockedUserRepository blockedUserRepository)
        {
            _logger = logger;
            _messageRepository = messageRepository;
            _blockedUserRepository = blockedUserRepository;
        }

        public async Task Consume(ConsumeContext<Message> context)
        {
            var getBlockedUser = await _blockedUserRepository.GetAsync(context.Message.To, context.Message.From);
            if (getBlockedUser == null)
            {
                await _messageRepository.AddAsync(context.Message);
                _logger.LogInformation($"Message received from : {context.Message.From} to : {context.Message.To}");
            }
            else
            {
                _logger.LogInformation(
                    $"Message can not sent that's already {context.Message.From} blocked : {context.Message.To}");
            }
        }
    }
}