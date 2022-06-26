using MongoDB.Driver;

namespace MessageService.Api
{
    public class BlockedUserRepository : IBlockedUserRepository
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<BlockUser> _collection;

        public BlockedUserRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<BlockUser>("blocked-users");
        }

        public async Task<BlockUser> GetAsync(string from, string to)
        {
            var blockedUser = await _collection.FindAsync(op => op.From == from && op.To == to);
            return blockedUser.FirstOrDefault();
        }

        public async Task AddAsync(BlockUser blockedUser)
        {
            await _collection.InsertOneAsync(blockedUser);
        }

        public async Task RemoveAsync(string from, string to)
        {
            await _collection.DeleteOneAsync(op => op.From == from && op.To == to);
        }
    }
}