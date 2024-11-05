using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TenNhaCungCap { get; set; }

        public string DiaChi { get; set; }

        public string? SoDienThoai { get; set; }

        public ICollection<Product>? products { get; set; }
    }
}
