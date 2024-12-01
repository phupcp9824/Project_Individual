using Data.DTO;
using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Front_end.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _client;


        public CustomerController(HttpClient httpClient, IHttpClientFactory client)
        {
            _httpClient = httpClient;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name = null, string type = null)
        {
            try
            {
                // Construct the base API URL
                var query = new StringBuilder("https://localhost:7214/api/Customer");

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

        [HttpGet]
        public async Task<IActionResult> DetailIndex(string name)
        {
            var query = new StringBuilder("https://localhost:7214/api/Role?");
            if (!string.IsNullOrWhiteSpace(name))
                query.Append($"name={name}&");
            var Product = await _httpClient.GetFromJsonAsync<List<Role>>(query.ToString());
            return View(Product);
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
        public async Task<IActionResult> Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"); // /ma hóa UTF8/ định là json
            var response = await _httpClient.PostAsync("https://localhost:7214/api/User/Register", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "User registered successfully!";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid input data.";
                return View();
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7214/");
            //Chỉ định ứng dụng nhận được response dưới dạng JSON.
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Send request to API
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7214/api/User/Login", model);

            if (response.IsSuccessStatusCode)
            {
                // Read the response
                var result = await response.Content.ReadFromJsonAsync<LoginModel>();

                if (result != null)
                {
                    var token = result.Token;
                    var userName = result.UserName;
                    var role = result.userRole;
                    var userId = result.userID.ToString();

                    // lưu infor vào cookie
                    Response.Cookies.Append("Cookie", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });

                    Response.Cookies.Append("userRoles", role, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });

                    Response.Cookies.Append("userName", userName, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });

                    Response.Cookies.Append("userId", userId, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });
                    if (role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (role == "Customer")
                    {
                        return RedirectToAction("Index", "Customer");
                    }
                    else
                    {
                        ViewBag.Error = "Invalid role.";
                        return View();
                    }
                }
            }
            return View();
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            try
            {
                var client = _client.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["Token"]);
                var response = await client.PostAsync("https://localhost:7214/api/User/logout", null);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to notify the server about logout.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error during logout: {ex.Message}");
            }
            // Remove additional cookies
            HttpContext.Response.Cookies.Delete("Cookie");
            HttpContext.Response.Cookies.Delete("userId");
            HttpContext.Response.Cookies.Delete("userRoles");
            HttpContext.Response.Cookies.Delete("userName");
            return RedirectToAction("Login", "Customer");
        }

    }
}
