using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Examintaion.Infrastructure.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _db;
        public MongoDBContext(IConfiguration configuration)
        {

            var connectionString = configuration.GetSection("mongo:ConnectionString").Value;
            var dbName = configuration.GetSection("mongo:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(dbName);

        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

    }
}
