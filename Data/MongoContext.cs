using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LottoApi.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("Lotto");
        }

        public IMongoCollection<LottoNumbers> LottoNumbers => _database.GetCollection<LottoNumbers>("LottoNumbers");
        public IMongoCollection<TicketNumbers> TicketNumbers => _database.GetCollection<TicketNumbers>("TicketNumbers");
        public IMongoCollection<Users> Users => _database.GetCollection<Users>("Users");
    }
}