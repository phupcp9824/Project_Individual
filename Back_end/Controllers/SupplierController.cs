using Data.IRepository;
using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Policy = "AdminPolicy")]
    public class SupplierController : ControllerBase
    {
        private readonly IRepSupplier _IrepSupplier;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(IRepSupplier irepSupplier, ILogger<SupplierController> logger)
        {
            _IrepSupplier = irepSupplier;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name)
        {
            var Listcate = await _IrepSupplier.GetAll(name);
            return Ok(Listcate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest("Category cannot be null.");
            }

            if (string.IsNullOrEmpty(supplier.Name))
            {
                return BadRequest("Category name is required.");
            }
            var add = await _IrepSupplier.Create(supplier   );
            return Ok(add);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Supplier supplier)
        {
            await _IrepSupplier.Update(supplier);
            return Ok(supplier);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var CateId = await _IrepSupplier.Delete(id);
            return Ok(CateId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var GetById = await _IrepSupplier.GetById(id);
            return Ok(GetById);
        }
    }
}
