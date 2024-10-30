using Data.IRepository;
using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepUser _IrepUser;
        private readonly ILogger<SizeController> _logger;

        public UserController(IRepUser repUser, ILogger<SizeController> logger)
        {
            _IrepUser = repUser;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name)
        {
            var Listcate = await _IrepUser.GetAll(name);
            return Ok(Listcate);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] User user)
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

    }
}
