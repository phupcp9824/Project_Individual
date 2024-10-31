using Microsoft.AspNetCore.Mvc;

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

    }
}
