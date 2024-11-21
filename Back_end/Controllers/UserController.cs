using Data.IRepository;
using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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


        public UserController(IRepUser repUser, ILogger<SizeController> logger, IConfiguration configuration)
        {
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
        public async Task<IActionResult> Create( User user)
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


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            var user = await _IrepUser.Login(loginModel);
            if (user == null)
            {
                return Ok(new LoginResponse
                {
                    Successfull = false,
                    Error = "Invalid Username and Password"
                });
            }

            // Generate tokens
            var tokenResult = CreateToken(user);


            return Ok(new LoginResponse
            {
                Successfull = true,
                Token = tokenResult.Token,
                UserName = loginModel.Username,
                RefreshToken = tokenResult.RefreshToken,
                Role = user.Role.Name,
                userID = user.Id.ToString()
            }) ;
        }

        private LoginResponse CreateToken(User user)
        {
            try
            {
                // Claims (yêu cầu) access token
                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"] ?? "DefaultSubject"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Username", user.Username),
                        new Claim(ClaimTypes.Role, user.Role?.Name)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));// mã hóa Token tránh giả mạo
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); // mã hóa để ký token, ensure server able generate valid token

                // Create access token
                var token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn
                );

                //Create refresh token / mà ko cần login lại
                var refreshToken = new RefreshToken
                {
                    Token = Guid.NewGuid().ToString(),
                    Expiration = DateTime.UtcNow.AddDays(7),
                    UserId = user.Id
                };

                // Convert token to string send to client
                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                return new LoginResponse
                {
                    Successfull = true,
                    Token = accessToken,
                    RefreshToken = refreshToken.Token,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating token");
                throw;
            }
        }
    }
}
