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

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketNumbers>> GetTicketHistoryById(string id)
        {
            var ticket = await _collection.Find(th => th.UserId == id).FirstOrDefaultAsync();

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult<TicketNumbers>> CreateTicketHistory(TicketNumbers ticket)
        {
            await _collection.InsertOneAsync(ticket);
            return CreatedAtAction(nameof(GetTicketHistoryById), new { id = ticket.UserId }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TicketNumbers>> UpdateTicketHistory(string id, TicketNumbers ticket)
        {
            var existingTicket = await _collection.FindOneAndReplaceAsync(th => th.UserId == id, ticket);

            if (existingTicket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketHistory(string id)
        {
            var result = await _collection.DeleteOneAsync(th => th.UserId == id);

            if (result.DeletedCount == 0)
                return NotFound();

            return NoContent();
        }
    }
}
