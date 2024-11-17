using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Front_end.Controllers
{
    public class SupplierController : Controller
    {
        private readonly HttpClient _httpClient;

        public SupplierController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> GetSupplier(string name = null)
        {
            var query = new StringBuilder("https://localhost:7214/api/Supplier?");
            if (!string.IsNullOrWhiteSpace(name))
                query.Append($"name={name}&");
            var sizes = await _httpClient.GetFromJsonAsync<List<Supplier>>(query.ToString());
            return View(sizes);
        }

        [HttpGet]
        public async Task<IActionResult> CreateSupplier()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(Supplier supplier)
        {
            // stringcontent tạo đối tượng content HTTP /convet về 1 đối tượng / mã hóa ký tự/ dữ liệu định dạnh json
            var content = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/Supplier", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction(nameof(GetSupplier));
            }
            return View(supplier);
        }

        [HttpGet]
        public async Task<IActionResult> DetailSupplier(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Size ID is required.");
            }

            try
            {
                var Sizes = await _httpClient.GetFromJsonAsync<Supplier>($"https://localhost:7214/api/Supplier/{id}");
                return View(Sizes);
            }
            catch (HttpRequestException ex)
            {
                return NotFound($"Error retrieving Size with ID {id}: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditSupplier(int? id)
        {
            // get đến api and tự thuần hóa json thành đối tượng
            var Sizes = await _httpClient.GetFromJsonAsync<Supplier>($"https://localhost:7214/api/Supplier/{id}");
            return View(Sizes);
        }

        [HttpPost]
        public async Task<IActionResult> EditSupplier(Supplier supplier)
        {
            var content = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7214/api/Supplier", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetSupplier));
            }
            return View(supplier);
        }

        public async Task<IActionResult> DeleteSupplier(int? id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7214/api/Supplier/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete Success";
                return RedirectToAction(nameof(GetSupplier));
            }
            return BadRequest("Error");
        }
    }
}
