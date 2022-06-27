using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

[ExcludeFromCodeCoverage]
public class MongoDBContext : IMongoDBContext
{
    private IMongoDatabase _db { get; set; }
    private IMongoClient _mongoClient { get; set; }

    public MongoDBContext(IOptions<Mongosettings> configuration)
    {
        _mongoClient = new MongoClient(configuration.Value.Connection);
        _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }

        return _db.GetCollection<T>(name);
    }
}