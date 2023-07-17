using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LottoApi.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public IMongoCollection<LottoNumbers> LottoNumbers => _database.GetCollection<LottoNumbers>("LottoNumbers");
        public IMongoCollection<TicketNumbers> TicketNumbers => _database.GetCollection<TicketNumbers>("TicketNumbers");
        public IMongoCollection<Users> Users => _database.GetCollection<Users>("Users");
    }
}