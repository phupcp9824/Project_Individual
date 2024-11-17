using Data.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Data.ViewModel
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; } = new Product();

        public List<SelectListItem> Sizes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
    }
}
