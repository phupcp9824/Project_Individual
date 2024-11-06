using Data.IRepository;
using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepCategory _IrepCategory;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IRepCategory irepCategory, ILogger<CategoryController> logger)
        {
            _IrepCategory = irepCategory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name)
        {
            var Listcate = await _IrepCategory.GetAll(name);
            return Ok(Listcate);
        }

        [HttpPost]
        public async Task<IActionResult> Create( Category category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null.");
            }

            if (string.IsNullOrEmpty(category.Name))
            {
                return BadRequest("Category name is required.");
            }
            var add = await _IrepCategory.Create(category);
            return Ok(add);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            await _IrepCategory.Update(category);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var CateId = await _IrepCategory.Delete(id);
            return Ok(CateId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var GetById = await _IrepCategory.GetById(id);
            return Ok(GetById);
        }      
    }
}
