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

        [HttpGet("/api/Users")]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            var users = await _collection.Find(u => true).ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(string id)
        {
            var user = await _collection.Find(u => u._id == id).FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginRequest>> Login(UserLoginRequest request)
        {
            var user = await _collection.Find(u => u.Email == request.Email && u.Password == request.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                // User not found or invalid credentials
                return Unauthorized();
            }

            // Create a UserLoginResponse object with the necessary data
            var response = new UserLoginRequest
            {
                Email = user.Email,
                Password = user.Password,
                // Include any other properties you want to return in the response
            };

            return Ok(response);
        }


        [HttpPost("register")]
        public async Task<ActionResult<Users>> RegisterUser(RegisterRequest request)
        {
            var existingUser = await _collection.Find(u => u.Email == request.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                // User with the same email already exists
                return Conflict("User with the same email already exists.");
            }

            var user = new Users
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password,
                IdentityNumber = request.IdentityNumber
                // Include any other properties you want to set for the user
            };

            await _collection.InsertOneAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user._id }, user);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> UpdateUser(string id, Users user)
        {
            var existingUser = await _collection.FindOneAndReplaceAsync(u => u._id == id, user);

            if (existingUser == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _collection.DeleteOneAsync(u => u._id == id);

            if (result.DeletedCount == 0)
                return NotFound();

            return NoContent();
        }
    }
}
