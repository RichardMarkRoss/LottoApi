using LottoApi.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LottoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<Users> _collection;

        public UsersController(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
            _collection = _dbContext.Users;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            var users = await _collection.Find(u => true).ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(string id)
        {
            var user = await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(Users user)
        {
            await _collection.InsertOneAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> UpdateUser(string id, Users user)
        {
            var existingUser = await _collection.FindOneAndReplaceAsync(u => u.Id == id, user);

            if (existingUser == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _collection.DeleteOneAsync(u => u.Id == id);

            if (result.DeletedCount == 0)
                return NotFound();

            return NoContent();
        }
    }
}
