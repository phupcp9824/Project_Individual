using Data.Model;
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


        //
        // Part Product
        //


        [HttpGet]
        public async Task<IActionResult> GetProduct(string name = null)
        {
            try
            {
                var query = new StringBuilder("https://localhost:7214/api/Product?");
                if (!string.IsNullOrWhiteSpace(name))
                    query.Append($"name={Uri.EscapeDataString(name)}&"); //Uri.EscapeDataString(name) mã hóa ký tự đặc biệt in name

                // Attempt to fetch data from the API
                var products = await _httpClient.GetFromJsonAsync<List<Product>>(query.ToString());
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
                // Log the error
                Console.WriteLine($"Unexpected error: {ex.Message}");
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                return View(new List<Product>());
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var Sizes = await _httpClient.GetFromJsonAsync<List<Data.Model.Size>>("https://localhost:7214/api/Size");
            ViewBag.Size = new SelectList(Sizes, "Id", "Name");

            var Category = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7214/api/Category");
            ViewBag.Category = new SelectList(Category, "Id", "Name");

            var Supplier = await _httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7214/api/Supplier");
            ViewBag.Supplier = new SelectList(Supplier, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product, IFormFile imageURL, List<int> ProductSizes, List<int> productCategories) // get list id ProductSizes and  productCategories để choose cho sp
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            // Handle image upload
            if (imageURL != null && imageURL.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var unique = Guid.NewGuid().ToString() + "_" + imageURL.FileName;
                var filepath = Path.Combine(path, unique);

                // lưu tệp image
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await imageURL.CopyToAsync(stream);
                }

                product.Image = "/images/" + unique;
            }


            if (ProductSizes != null && ProductSizes.Any()) // check null và có chứa ptu nao ko any
            {
                // ProductSizes.Select chiếu ptu của 1 tap hop thanh 1 dạng mới LINQ
                product.ProductSizes = ProductSizes.Select(sizeId => new ProductSize { SizeId = sizeId }).ToList(); // chuyển đổi select thành 1 ToList
            }


            if (productCategories != null && productCategories.Any())
            {
                product.productCategories = productCategories.Select(categoryId => new ProductCategory { CategoryId = categoryId }).ToList();
            }

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7214/api/Product", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create Success";
                return RedirectToAction(nameof(GetProduct));
            }
            else
            {
                TempData["ErrorMessage"] = "Error creating product: " + await response.Content.ReadAsStringAsync();
                return View(product);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailProduct(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Size ID is required.");
            }

            try
            {
                var Sizes = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7214/api/Product/{id}");
                return View(Sizes);
            }
            catch (HttpRequestException ex)
            {
                return NotFound($"Error retrieving Size with ID {id}: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            var Sizes = await _httpClient.GetFromJsonAsync<List<Data.Model.Size>>("https://localhost:7214/api/Size");
            ViewBag.Size = new SelectList(Sizes, "Id", "Name");

            var Category = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7214/api/Category");
            ViewBag.Category = new SelectList(Category, "Id", "Name");

            var Supplier = await _httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7214/api/Supplier");
            ViewBag.Supplier = new SelectList(Supplier, "Id", "Name");

            // get đến api and tự thuần hóa json thành đối tượng
            var Size = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7214/api/Product/{id}");
            return View(Size);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, IFormFile imageURL, List<int> ProductSizes, List<int> productCategories)
        {
            // Fetch the existing product data from the API
            var existingProduct = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7214/api/Product/{product.Id}");

            if (existingProduct == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction(nameof(GetProduct));
            }

            //
            if (imageURL != null && imageURL.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var unique = Guid.NewGuid().ToString() + "_" + imageURL.FileName;
                var filepath = Path.Combine(path, unique);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await imageURL.CopyToAsync(stream);
                }

                product.Image = "/images/" + unique;
            }
            else
            {
                // nếu ko thay đổi hình ảnh thì dữ nguyên ảnh cũ
                product.Image = existingProduct.Image;
            }

            // update size nếu provider ko dữ nguyên
            if (ProductSizes != null && ProductSizes.Any())
            {
                product.ProductSizes = ProductSizes.Select(sizeId => new ProductSize { SizeId = sizeId }).ToList();
            }
            else
            {
                product.ProductSizes = existingProduct.ProductSizes;
            }

            // update Category nếu provider ko dữ nguyên
            if (productCategories != null && productCategories.Any())
            {
                product.productCategories = productCategories.Select(categoryId => new ProductCategory { CategoryId = categoryId }).ToList();
            }
            else
            {
                product.productCategories = existingProduct.productCategories;
            }

            // nếu ko updete supplier dữ nguyên cũ
            if (product.SupplierId == null)
            {
                product.SupplierId = existingProduct.SupplierId;
            }

            // update sp
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7214/api/Product", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Success";
                return RedirectToAction(nameof(GetProduct));
            }

            TempData["ErrorMessage"] = "Error updating product: " + await response.Content.ReadAsStringAsync();
            return View(product);
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7214/api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete Success";
                return RedirectToAction(nameof(GetProduct));
            }
            return BadRequest("Error");
        }


        //
        // Part Role
        //

        [HttpGet]
        public async Task<IActionResult> GetRole(string name = null)
        {
            List<Role> roles = new List<Role>();
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Role/Getall");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                roles = JsonConvert.DeserializeObject<List<Role>>(data);
            }
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(Role? role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            var data = JsonConvert.SerializeObject(role);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7214/api/Role/Create", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Role created successfully.";
                return RedirectToAction(nameof(GetRole));
            }

            TempData["ErrorMessage"] = "Error creating role: " + await response.Content.ReadAsStringAsync();
            return View(role);
        }


        [HttpGet]
        public async Task<IActionResult> DetailRole(int? id)
        {
            Role role = new Role();
            // Send GET request to the API to get user by id
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Role/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                // đọc content  từ api
                string data = await response.Content.ReadAsStringAsync();
                role = JsonConvert.DeserializeObject<Role>(data); // chuyển chuỗi json về 1 đối tượng
            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(int? id)
        {
            Role role = new Role();
            // Send GET request to the API to get user by id
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Role/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                // đọc content  từ api
                string data = await response.Content.ReadAsStringAsync();
                role = JsonConvert.DeserializeObject<Role>(data); // chuyển chuỗi json về 1 đối tượng
            }
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(Role role)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + "/Role/Update", role);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Edit Success";
                return RedirectToAction(nameof(GetRole));
            }
            return View(role);
        }

        public async Task<IActionResult> DeleteRole(int? id)
        {
            try
            {
                // get request delete to APi
                HttpResponseMessage response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/Role/Delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User deleted successfully";
                    return RedirectToAction(nameof(GetRole));

                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete user.";
                    return RedirectToAction(nameof(GetRole));
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        //
        // Part User
        //


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
            var Roles = await _httpClient.GetFromJsonAsync<List<Role>>("https://localhost:7214/api/Role/Getall");
            ViewBag.Roles = new SelectList(Roles, "Id", "Name");

            var User = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7214/api/User/{id}");

            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            var ExistingUser = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7214/api/User/{user.Id}");
                // not update role thì giữu nguyên
            if(user.RoleId == null)
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



        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
