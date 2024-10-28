using Data.IRepository;
using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly IRepSize _repSize;
        private readonly ILogger<SizeController> _logger;

        public SizeController(IRepSize repSize, ILogger<SizeController> logger)
        {
            _repSize = repSize;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name)
        {
            var Listcate = await _repSize.GetAll(name);
            return Ok(Listcate);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Size size)
        {
            if (size == null)
            {
                return BadRequest("Category cannot be null.");
            }

            if (string.IsNullOrEmpty(size.Name))
            {
                return BadRequest("Category name is required.");
            }
            var add = await _repSize.Create(size);
            return Ok(add);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Size size)
        {
            await _repSize.Update(size);
            return Ok(size);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var CateId = await _repSize.Delete(id);
            return Ok(CateId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var GetById = await _repSize.GetById(id);
            return Ok(GetById);
        }
    }
}
