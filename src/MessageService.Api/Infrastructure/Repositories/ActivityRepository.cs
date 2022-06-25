using MongoDB.Driver;

namespace MessageService.Api
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<Activity> _collection;

        public ActivityRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<Activity>("activities");
        }

        public async Task AddAsync(Activity activity)
        {
            await _collection.InsertOneAsync(activity);
        }
    }

}