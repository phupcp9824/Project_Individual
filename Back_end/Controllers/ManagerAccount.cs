using Data.IRepository;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Policy = "AdminPolicy")]
    public class ManagerAccount : ControllerBase
    {
        private readonly IRepUser _IrepUser;
        private readonly ILogger<SizeController> _logger;
        private readonly OrderDbContext _db;

        public ManagerAccount(IRepUser repUser, ILogger<SizeController> logger, OrderDbContext db)
        {
            _IrepUser = repUser;
            _logger = logger;
            _db = db;
        }

        [HttpGet]

        public async Task<IActionResult> Getall(string? name)
        {
            try
            {
                var listUser = await _IrepUser.GetAll(name);
                return Ok(listUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _IrepUser.Update(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var CateId = await _IrepUser.Delete(id);
            return Ok(CateId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var GetById = await _IrepUser.GetById(id);
            return Ok(GetById);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = _db.users.FirstOrDefault(e => e.Email == model.Email || e.Username == model.Username);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "Email or Username already exists" });
            }
            // băm the password before storing it
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                FullName = model.FullName,
                Username = model.Username,
                Password = hashedPassword,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address ?? string.Empty,
                RoleId = 2
            };
            await _db.users.AddAsync(user);
            await _db.SaveChangesAsync();
            return Ok(new { Message = "User registered successfully!" });
        }

    }
}
