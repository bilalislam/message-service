using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;

namespace MessageService.Api
{
    [ExcludeFromCodeCoverage]
    public class BlockedUserRepository : IBlockedUserRepository
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<BlockUser> _collection;

        public BlockedUserRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<BlockUser>("blocked-users");
        }

        public async Task<BlockUser> GetAsync(string from, string to, CancellationToken cancellationToken)
        {
            var blockedUser = await _collection.FindAsync(op => op.From == from && op.To == to,
                cancellationToken: cancellationToken);
            return blockedUser.FirstOrDefault();
        }

        public async Task AddAsync(BlockUser blockedUser, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(blockedUser, cancellationToken: cancellationToken);
        }

        public async Task RemoveAsync(string from, string to, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(op => op.From == from && op.To == to,
                cancellationToken: cancellationToken);
        }
    }
}