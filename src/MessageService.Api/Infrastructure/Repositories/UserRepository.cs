using MongoDB.Driver;

namespace MessageService.Api;

public class UserRepository : IUserRepository
{
    private readonly IMongoDBContext _context;
    private readonly IMongoCollection<User> _collection;


    public UserRepository(IMongoDBContext context)
    {
        _context = context;
        _collection = _context.GetCollection<User>("users");
    }


    public async Task<User> GetAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _collection.FindAsync(op => op.Email == email, cancellationToken: cancellationToken);
        return user.FirstOrDefault();
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(n => n.Id.Equals(user.Id), user, new ReplaceOptions() { IsUpsert = false },
            cancellationToken);
    }
}