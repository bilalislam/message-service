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

        public Task<List<string>> GetUsersAsync(string from, CancellationToken cancellationToken)
        {
            // var users = _collection.Aggregate()
            //     .Match(new BsonDocument() { { "From", from } })
            //     .Group(new BsonDocument { { "_id", "$To" } })
            //     .Project<string>(new BsonDocument()
            //     {
            //         { "_id", "$To" }
            //     }).ToList();
            //
            // return users;
            return null;
        }

        public async Task<List<Message>> GetHistoriesAsync(string from, string to, CancellationToken cancellationToken)
        {
            var messages = await _collection.FindAsync(op => op.From == from && op.To == to,
                cancellationToken: cancellationToken);
            return messages.ToList();
        }
    }
}