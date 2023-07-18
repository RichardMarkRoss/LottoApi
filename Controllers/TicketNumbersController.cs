using LottoApi.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LottoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketNumbersController : ControllerBase
    {
        private readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<TicketNumbers> _collection;

        public TicketNumbersController(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
            _collection = _dbContext.TicketNumbers;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketNumbers>>> GetAllTicketHistory()
        {
            var ticketHistory = await _collection.Find(th => true).ToListAsync();
            return Ok(ticketHistory);
        }

        [HttpPost]
        public async Task<ActionResult<TicketNumbers>> CreateTicketHistory(TicketNumbers ticket)
        {
            await _collection.InsertOneAsync(ticket);
            return CreatedAtAction(nameof(GetAllTicketHistory), new { id = ticket.UserId }, ticket);
        }
    }
}
