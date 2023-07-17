using System.Threading.Tasks;
using LottoApi.Data;
using MongoDB.Driver;

namespace MongoDBService
{
    public class MongoDbService
    {
        private readonly MongoDbContext _dbContext;

        public MongoDbService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertLottoNumbers(LottoNumbers numbers)
        {
            await _dbContext.LottoNumbers.InsertOneAsync(numbers);
        }

        public async Task InsertTicketNumbers(TicketNumbers numbers)
        {
            await _dbContext.TicketNumbers.InsertOneAsync(numbers);
        }

        public async Task InsertUser(Users user)
        {
            await _dbContext.Users.InsertOneAsync(user);
        }
    }
}
