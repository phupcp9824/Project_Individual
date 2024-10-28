using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name must from 3 - 25 characters")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Description must be under 256 characters")]
        public string? Description { get; set; }

        public ICollection<ProductCategory>? productCategories { get; set; }

    }
}
