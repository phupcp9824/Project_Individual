using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "MaProduct must from 6 - 30 characters")]
        public string MaProduct {  get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(35, MinimumLength = 6, ErrorMessage = "NameProduct must from 6 - 35 characters")]
        public string NameProduct { get; set; }
        public string Image {  get; set; }

        [Range(0, 10000000, ErrorMessage = "Price must be between 0 and 10.000.000")]
        public double Price { get; set; }

        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1.000")]
        public int Quantity { get; set; }

        [StringLength(255, ErrorMessage = "Material must be under 256 characters")]
        public string? Material { get; set; }

        [StringLength(255, ErrorMessage = "Status must be under 256 characters")]
        public string? Status { get; set; }
            
        [StringLength(255, ErrorMessage = "Description must be under 256 characters")]
        public string? Description { get; set; }

        public ICollection<ProductSize>? ProductSizes { get; set; }
        public ICollection<ProductCategory>? productCategories { get; set; }   

        public ICollection<OrderDetail>? orderDetails { get; set; }

        public int? SupplierId { get; set; }
        public Supplier? Suppliers { get; set; }
    }
}
