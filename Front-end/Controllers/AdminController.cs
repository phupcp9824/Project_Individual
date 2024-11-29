using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Front_end.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly Uri BaseAddress = new Uri("https://localhost:7214/api");

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = BaseAddress;
        }
        // update
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
