using MongoDB.Driver;

public interface IMongoDBContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}
