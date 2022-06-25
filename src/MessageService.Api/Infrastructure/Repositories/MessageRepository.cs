using MongoDB.Driver;

namespace MessageService.Api
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<Message> _collection;

        public MessageRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<Message>("messages");
        }

        public async Task AddAsync(Message message)
        {
            await _collection.InsertOneAsync(message);
        }
    }
}