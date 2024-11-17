using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;

namespace Front_end.Controllers
{
    public class RoleController : Controller
    {
        private readonly HttpClient _httpClient;

        public RoleController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetRole(string name = null)
        {
            var query = new StringBuilder("https://localhost:7214/api/Role?");
            if (!string.IsNullOrWhiteSpace(name))
                query.Append($"name={name}&");
            var sizes = await _httpClient.GetFromJsonAsync<List<Role>>(query.ToString());
            return View(sizes);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(Role? role)
        {
            // stringcontent tạo đối tượng content HTTP /convet về 1 đối tượng / mã hóa ký tự/ dữ liệu định dạnh json
            var content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/Role", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction(nameof(GetRole));
            }
            return View(role);
        }


        [HttpGet]
        public async Task<IActionResult> DetailRole(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Size ID is required.");
            }

            try
            {
                var Roles = await _httpClient.GetFromJsonAsync<Role>($"https://localhost:7214/api/Role/{id}");
                return View(Roles);
            }
            catch (HttpRequestException ex)
            {
                return NotFound($"Error retrieving Size with ID {id}: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(int? id)
        {
            // get đến api and tự thuần hóa json thành đối tượng
            var Roles = await _httpClient.GetFromJsonAsync<Role>($"https://localhost:7214/api/Role/{id}");
            return View(Roles);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(Role role)
        {
            var content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7214/api/Role", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetRole));
            }
            return View(role);
        }

        public async Task<IActionResult> DeleteRole(int? id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7214/api/Role/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Delete Success";
                    return RedirectToAction(nameof(GetRole));
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
