using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Front_end.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;


        public async Task<IActionResult> Index()
        {
            return View();
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
        public async Task<IActionResult> Signup(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user); // Return the view with the model errors
            }
            // StringContent đại diện cho content in stirng http
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"); //application/json cho biết rằng nội dung đang được gửi là dữ liệu JSON
            var response = await _httpClient.PostAsync("https://localhost:7214/api/User", content);
            if(response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
