using Data.DTO;
using Data.IRepository;
using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepUser _IrepUser;
        private readonly ILogger<SizeController> _logger;
        private readonly IConfiguration _configuration;

        private readonly OrderDbContext _db;
        public UserController(IRepUser repUser, ILogger<SizeController> logger, IConfiguration configuration, OrderDbContext db)
        {
            _db = db;
            _IrepUser = repUser;
            _logger = logger;
            _configuration = configuration;
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


        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (user == null)
            {
                return BadRequest("Category cannot be null.");
            }

            if (string.IsNullOrEmpty(user.FullName))
            {
                return BadRequest("Category name is required.");
            }
            var add = await _IrepUser.Create(user);
            return Ok(add);
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var User = _db.users.Include(u => u.Role).FirstOrDefault(u => u.Username == model.UserName);
            if (User == null || !BCrypt.Net.BCrypt.Verify(model.Password, User.Password))
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }
            
            // Generate the JWT token for the user
            var token = GenerateJwtToken(User.Username, User.Role.Name, User.Id);

            // Set the JWT token in an HttpOnly cookie
            Response.Cookies.Append("Token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromMinutes(60)
            });

            // Return a response with user details and the token
            return Ok(new
            {
                Token = token,
                UserName = User.Username,
                UserRole = User.Role.Name,
                UserId = User.Id
            });
        }

        private string GenerateJwtToken(string user, string userRole, int userId)
        {
            var jwtKey = _configuration["Jwt:SecurityKey"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("Jwt:Key is not configured properly.");
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user),
            new Claim(ClaimTypes.Role, userRole ?? "User"),
            new Claim("UserId", userId.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("Shoe_Store_Cookie");

            return Ok(new { Message = "Logout Success." });
        }
    }
}

