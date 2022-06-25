using MassTransit;

namespace MessageService.Api
{
    public class MessageConsumer :
        IConsumer<Message>
    {
        readonly ILogger<MessageConsumer> _logger;
        private readonly IMongoDBContext _mongoClient;
        private readonly IMessageRepository _messageRepository;

        public MessageConsumer(ILogger<MessageConsumer> logger, IMessageRepository messageRepository)
        {
            _logger = logger;
            _messageRepository = messageRepository;
        }

        public async Task Consume(ConsumeContext<Message> context)
        {
            await _messageRepository.AddAsync(context.Message);
            _logger.LogInformation($"Message received from : {context.Message.From} to : {context.Message.To}");
        }
    }
}