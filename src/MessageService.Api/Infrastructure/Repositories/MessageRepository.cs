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

        public async Task AddAsync(Message message, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(message, cancellationToken);
        }

        public async Task<List<Message>> GetUsersAsync(string from, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return _collection.AsQueryable()
                    .Where(p => p.From == from)
                    .Select(x => new Message(x.Id, x.From, x.To, x.Msg, x.EventOn))
                    .GroupBy(p => p.To)
                    .ToList()
                    .Select(p => Message.SetTo(p.Key))
                    .ToList();
            });
        }

        public async Task<List<Message>> GetHistoriesAsync(string from, string to, CancellationToken cancellationToken)
        {
            var messages = await _collection.FindAsync(op => op.From == from && op.To == to,
                cancellationToken: cancellationToken);
            return messages.ToList();
        }
    }
}