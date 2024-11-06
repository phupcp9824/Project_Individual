using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;

namespace Front_end.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //
        // Part Size
        //
        [HttpGet]
        public async Task<IActionResult> GetSize(string name = null)
        {
            var query = new StringBuilder("https://localhost:7214/api/Size?");
            if (!string.IsNullOrWhiteSpace(name))
                query.Append($"name={name}&");
            var sizes = await _httpClient.GetFromJsonAsync<List<Data.Model.Size>>(query.ToString());
            return View(sizes);
        }

        [HttpGet]
        public async Task<IActionResult> CreateSize()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSize(Data.Model.Size size)
        {
            // stringcontent tạo đối tượng content HTTP /convet về 1 đối tượng / mã hóa ký tự/ dữ liệu định dạnh json
            var content = new StringContent(JsonConvert.SerializeObject(size), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/Size", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction(nameof(GetSize));
            }
            return View(size);
        }

        [HttpGet]
        public async Task<IActionResult> DetailSize(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Size ID is required.");
            }

            try
            {
                var Sizes = await _httpClient.GetFromJsonAsync<Data.Model.Size>($"https://localhost:7214/api/Size/{id}");
                return View(Sizes);
            }
            catch (HttpRequestException ex)
            {
                return NotFound($"Error retrieving Size with ID {id}: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditSize(int? id)
        {
            // get đến api and tự thuần hóa json thành đối tượng
            var Sizes = await _httpClient.GetFromJsonAsync<Data.Model.Size>($"https://localhost:7214/api/Size/{id}");
            return View(Sizes);
        }

        [HttpPost]
        public async Task<IActionResult> EditSize(Data.Model.Size size)
        {
            var content = new StringContent(JsonConvert.SerializeObject(size), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7214/api/Size", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetSize));
            }
            return View(size);
        }

        public async Task<IActionResult> DeleteSize(int? id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7214/api/Size/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetSize));
            }
            return BadRequest("Error");
        }


        //
        // Part Category
        //

        [HttpGet]
        public async Task<IActionResult> GetCategory(string name = null)
        {
            var query = new StringBuilder("https://localhost:7214/api/Category?");
            if (!string.IsNullOrWhiteSpace(name))
                query.Append($"name={name}&");
            var sizes = await _httpClient.GetFromJsonAsync<List<Category>>(query.ToString());
            return View(sizes);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            // stringcontent tạo đối tượng content HTTP /convet về 1 đối tượng / mã hóa ký tự/ dữ liệu định dạnh json
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/Category", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction(nameof(GetCategory));
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> DetailCategory(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Size ID is required.");
            }

            try
            {
                var Sizes = await _httpClient.GetFromJsonAsync<Category>($"https://localhost:7214/api/Category/{id}");
                return View(Sizes);
            }
            catch (HttpRequestException ex)
            {
                return NotFound($"Error retrieving Size with ID {id}: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditCategory(int? id)
        {
            // get đến api and tự thuần hóa json thành đối tượng
            var Sizes = await _httpClient.GetFromJsonAsync<Category>($"https://localhost:7214/api/Category/{id}");
            return View(Sizes);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7214/api/Category", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetCategory));
            }
            return View(category);
        }
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7214/api/Category/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete Success";
                return RedirectToAction(nameof(GetCategory));
            }
            return BadRequest("Error");
        }


        //
        // Part Supplier
        //
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



        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
