using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Front_end.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(string name = null)
        {
            try
            {
                var query = new StringBuilder("https://localhost:7214/api/Product?");
                if (!string.IsNullOrWhiteSpace(name))
                    query.Append($"name={Uri.EscapeDataString(name)}&"); //Uri.EscapeDataString(name) mã hóa ký tự đặc biệt in name

                // Attempt to fetch data from the API
                var products = await _httpClient.GetFromJsonAsync<List<Product>>(query.ToString());
                return View(products);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                TempData["ErrorMessage"] = "Could not retrieve products at this time. Please try again later.";
                return View(new List<Product>());
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Unexpected error: {ex.Message}");
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                return View(new List<Product>());
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var Sizes = await _httpClient.GetFromJsonAsync<List<Size>>("https://localhost:7214/api/Size");
            ViewBag.Size = new SelectList(Sizes, "Id", "Name");

            var Category = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7214/api/Category");
            ViewBag.Category = new SelectList(Category, "Id", "Name");

            var Supplier = await _httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7214/api/Supplier");
            ViewBag.Supplier = new SelectList(Supplier, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product, IFormFile imageURL, List<int> ProductSizes, List<int> productCategories) // get list id ProductSizes and  productCategories để choose cho sp
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            // Handle image upload
            if (imageURL != null && imageURL.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var unique = Guid.NewGuid().ToString() + "_" + imageURL.FileName;
                var filepath = Path.Combine(path, unique);

                // lưu tệp image
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await imageURL.CopyToAsync(stream);
                }

                product.Image = "/images/" + unique;
            }


            if (ProductSizes != null && ProductSizes.Any()) // check null và có chứa ptu nao ko any
            {
                // ProductSizes.Select chiếu ptu của 1 tap hop thanh 1 dạng mới LINQ
                product.ProductSizes = ProductSizes.Select(sizeId => new ProductSize { SizeId = sizeId }).ToList(); // chuyển đổi select thành 1 ToList
            }


            if (productCategories != null && productCategories.Any())
            {
                product.productCategories = productCategories.Select(categoryId => new ProductCategory { CategoryId = categoryId }).ToList();
            }

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/Product", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction(nameof(GetProduct));
            }
            else
            {
                TempData["ErrorMessage"] = "Error creating product: " + await response.Content.ReadAsStringAsync();
                return View(product);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailProduct(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Size ID is required.");
            }

            try
            {
                var Sizes = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7214/api/Product/{id}");
                return View(Sizes);
            }
            catch (HttpRequestException ex)
            {
                return NotFound($"Error retrieving Size with ID {id}: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            var Sizes = await _httpClient.GetFromJsonAsync<List<Data.Model.Size>>("https://localhost:7214/api/Size");
            ViewBag.Size = new SelectList(Sizes, "Id", "Name");

            var Category = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7214/api/Category");
            ViewBag.Category = new SelectList(Category, "Id", "Name");

            var Supplier = await _httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7214/api/Supplier");
            ViewBag.Supplier = new SelectList(Supplier, "Id", "Name");

            // get đến api and tự thuần hóa json thành đối tượng
            var Size = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7214/api/Product/{id}");
            return View(Size);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, IFormFile imageURL, List<int> ProductSizes, List<int> productCategories)
        {
            // Fetch the existing product data from the API
            var existingProduct = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7214/api/Product/{product.Id}");

            if (existingProduct == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction(nameof(GetProduct));
            }

            //
            if (imageURL != null && imageURL.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var unique = Guid.NewGuid().ToString() + "_" + imageURL.FileName;
                var filepath = Path.Combine(path, unique);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await imageURL.CopyToAsync(stream);
                }

                product.Image = "/images/" + unique;
            }
            else
            {
                // nếu ko thay đổi hình ảnh thì dữ nguyên ảnh cũ
                product.Image = existingProduct.Image;
            }

            // update size nếu provider ko dữ nguyên
            if (ProductSizes != null && ProductSizes.Any())
            {
                product.ProductSizes = ProductSizes.Select(sizeId => new ProductSize { SizeId = sizeId }).ToList();
            }
            else
            {
                product.ProductSizes = existingProduct.ProductSizes;
            }

            // update Category nếu provider ko dữ nguyên
            if (productCategories != null && productCategories.Any())
            {
                product.productCategories = productCategories.Select(categoryId => new ProductCategory { CategoryId = categoryId }).ToList();
            }
            else
            {
                product.productCategories = existingProduct.productCategories;
            }

            // nếu ko updete supplier dữ nguyên cũ
            if (product.SupplierId == null)
            {
                product.SupplierId = existingProduct.SupplierId;
            }

            // update sp
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7214/api/Product", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetProduct));
            }

            TempData["ErrorMessage"] = "Error updating product: " + await response.Content.ReadAsStringAsync();
            return View(product);
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7214/api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Delete Success";
                    return RedirectToAction(nameof(GetProduct));
                }
                return BadRequest("Error");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
