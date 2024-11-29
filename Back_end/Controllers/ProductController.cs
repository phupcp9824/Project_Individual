﻿using Data.IRepository;
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
    public class ProductController : ControllerBase
    {
        private readonly IRepProduct _repProduct;
        private readonly ILogger<SizeController> _logger;

        public ProductController(IRepProduct repProduct, ILogger<SizeController> logger)
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Category cannot be null.");
            }

            if (string.IsNullOrEmpty(product.NameProduct))
            {
                return BadRequest("Category name is required.");
            }
            var add = await _repProduct.Create(product);
            return Ok(add);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await _repProduct.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ProductId = await _repProduct.Delete(id);
            return Ok(ProductId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var GetById = await _repProduct.GetById(id);
            return Ok(GetById);
        }
    }
}
