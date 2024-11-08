using Data.IRepository;
using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepRole _IrepRole;
        private readonly ILogger<SizeController> _logger;

        public RoleController(IRepRole repRole, ILogger<SizeController> logger)
        {
            _IrepRole = repRole;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name)
        {
            var Listcate = await _IrepRole.GetAll(name);
            return Ok(Listcate);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Category cannot be null.");
            }

            if (string.IsNullOrEmpty(role.Name))
            {
                return BadRequest("Category name is required.");
            }
            try
            {
                var addedRole = await _IrepRole.Create(role);
                return Ok(addedRole);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role.");
                return StatusCode(500, "Internal server error while creating the role.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Role role)
        {
            try
            {
                await _IrepRole.Update(role);
                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role.");
                return StatusCode(500, "Internal server error while updating the role.");
            }
     
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var CateId = await _IrepRole.Delete(id);
            return Ok(CateId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var GetById = await _IrepRole.GetById(id);
            return Ok(GetById);
        }
    }
}
