using LottoApi.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LottoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LottoNumbersController : ControllerBase
    {
        private readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<LottoNumbers> _collection;

        public LottoNumbersController(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
            _collection = _dbContext.LottoNumbers;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LottoNumbers>>> GetAllLottoNumbers()
        {
            var lottoNumbers = await _collection.Find(ln => true).ToListAsync();
            return Ok(lottoNumbers);
        }

        // GET: api/LottoNumbers/search?drawDate=2023-07-01&gameType=Daily
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<LottoNumbers>>> GetFilteredTicketHistory(string drawDate, string gameType)
        {
            var lottoNumbers = await _collection.Find(th => th.DrawDate == drawDate && th.GameType == gameType).ToListAsync();

            if (lottoNumbers.Count() == 0)
                return NotFound();

            return Ok(lottoNumbers);
        }
    }
}