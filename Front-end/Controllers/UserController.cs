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

        [HttpGet]
        public async Task<IActionResult> GetUser(string name = null)
        {
            var query = new StringBuilder("https://localhost:7214/api/ManagerAccount?"); // string builder to construct URL for the API

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
            //var roles = await _httpClient.GetFromJsonAsync<Role>("https://localhost:7214/api/ManagerAccount");
            //// ViewBag truyền dữ liệu tới view
            //ViewBag.role = new SelectList((System.Collections.IEnumerable)roles, "Id", "Name");  // SelectList gán viewbag ép thành kiểu IEnumerable 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            user.RoleId = 2; // Default to 'customer' role ID
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/ManagerAccount/Register", content);

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
            var Sizes = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7214/api/ManagerAccount/{id}");
            return View(Sizes);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int? id)
        {
            var User = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7214/api/ManagerAccount/{id}");

            var Roles = await _httpClient.GetFromJsonAsync<List<Role>>("https://localhost:7214/api/Role");
            ViewBag.Roles = new SelectList(Roles, "Id", "Name");

            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            var ExistingUser = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7214/api/ManagerAccount/{user.Id}");
            // not update role thì giữu nguyên
            if (user.RoleId == null)
            {
                user.RoleId = ExistingUser.RoleId;
            }
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7214/api/ManagerAccount", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetUser));
            }
            return View(user);
        }

        public async Task<IActionResult> DeleteUser(int? id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7214/api/ManagerAccount/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete Success";
                return RedirectToAction(nameof(GetUser));
            }
            return BadRequest("Error");
        }
    }
}
