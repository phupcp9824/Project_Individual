using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public Product? Product { get; set; }
        public Category? Category { get; set; }
    }
}
