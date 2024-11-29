using Data.DTO;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text;

namespace Front_end.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid input data.";
                return View();
            }
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            // Send request to API
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7214/api/User/Login", content);

            if (response.IsSuccessStatusCode)
            {
                // Read the response
                var result = await response.Content.ReadFromJsonAsync<LoginModel>();

                if (result != null)
                {
                    var token = result.Token;
                    var userName = result.UserName;
                    var role = result.userRole;
                    var userId = result.userID?.ToString();

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

        [HttpGet]
        public async Task<IActionResult> GetUser(string name = null)
        {
            var query = new StringBuilder("https://localhost:7214/api/User?"); // string builder to construct URL for the API

            if (!string.IsNullOrWhiteSpace(name))
                query.Append($"name={Uri.EscapeDataString(name)}&");

            try
            {
                var response = await _httpClient.GetAsync(query.ToString()); // GetAsync allows checking status code
                if (response.IsSuccessStatusCode)
                {
                    var sizes = await response.Content.ReadFromJsonAsync<List<User>>();
                    return View(sizes);
                }
                else
                {
                    // Handle the error if status code is not successful
                    ModelState.AddModelError("", $"API call failed with status code: {response.StatusCode}");
                    return View(new List<User>());
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception here
                ModelState.AddModelError("", $"Error occurred: {ex.Message}");
                return View(new List<User>());
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            //var roles = await _httpClient.GetFromJsonAsync<Role>("https://localhost:7214/api/User");
            //// ViewBag truyền dữ liệu tới view
            //ViewBag.role = new SelectList((System.Collections.IEnumerable)roles, "Id", "Name");  // SelectList gán viewbag ép thành kiểu IEnumerable 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            user.RoleId = 2; // Default to 'customer' role ID
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/User", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction(nameof(GetUser));
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> DetailUser(int? id)
        {
            // get đến api and tự thuần hóa json thành đối tượng
            var Sizes = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7214/api/User/{id}");
            return View(Sizes);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int? id)
        {
            var User = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7214/api/User/{id}");

            var Roles = await _httpClient.GetFromJsonAsync<List<Role>>("https://localhost:7214/api/Role");
            ViewBag.Roles = new SelectList(Roles, "Id", "Name");

            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            var ExistingUser = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7214/api/User/{user.Id}");
            // not update role thì giữu nguyên
            if (user.RoleId == null)
            {
                user.RoleId = ExistingUser.RoleId;
            }
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7214/api/User", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetUser));
            }
            return View(user);
        }

        public async Task<IActionResult> DeleteUser(int? id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7214/api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete Success";
                return RedirectToAction(nameof(GetUser));
            }
            return BadRequest("Error");
        }
    }
}
