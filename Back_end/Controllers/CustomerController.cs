using Data.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private readonly IRepProduct _repProduct;
        private readonly ILogger<SizeController> _logger;

        public CustomerController(IRepProduct repProduct, ILogger<SizeController> logger)
        {
            _repProduct = repProduct;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? name)
        {
            try
            {
                var ListProduct = await _repProduct.GetAll(name);
                return Ok(ListProduct);
            }
            catch (Exception ex)
            {
                // Log the exception (for production, replace Console.WriteLine with a proper logging library)
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}
