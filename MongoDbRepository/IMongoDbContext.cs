using MongoDB.Driver;

namespace MongoDbRepository
{
    public interface IMongoDbContext
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        bool PluralizeDocumentName { get; set; }
        IMongoCollection<T> Collection<T>() where T : MongoEntity;
    }
}