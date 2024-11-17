using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Front_end.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

     

        public async Task<IActionResult> Trangchu()
        {
            return View();
        }
        public async Task<IActionResult> Gioithieu()
        {
            return View();
        }
        public async Task<IActionResult> Lienhe()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            user.RoleId = 2; // Default to 'customer' role ID
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/User", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction("");
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name = null, string type = null)
        {
            try
            {
                // Construct the base API URL
                var query = new StringBuilder("https://localhost:7214/api/Product");

                // Check if there are query parameters to append
                if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(type))
                {
                    query.Append("?");
                    if (!string.IsNullOrWhiteSpace(name))
                        query.Append($"name={Uri.EscapeDataString(name)}&"); // mã hóa ký tự đặc biệt

                    if (!string.IsNullOrWhiteSpace(type))
                        query.Append($"type={Uri.EscapeDataString(type)}&");

                    // Remove the last '&' character
                    query.Length--;
                }

                // Log the query URL for debugging
                Console.WriteLine($"API Request URL: {query}");

                // Fetch data from the API
                var products = await _httpClient.GetFromJsonAsync<List<Product>>(query.ToString());

                if (products == null || !products.Any())
                {
                    TempData["ErrorMessage"] = "No products available.";
                    return View(new List<Product>());
                }

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
                Console.WriteLine($"Unexpected error: {ex.Message}");
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                return View(new List<Product>());
            }
        }



    }
}
