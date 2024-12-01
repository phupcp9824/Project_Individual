using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Size
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Name must from 3 - 10 characters")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Description must be under 256 characters")]
        public string? Description { get; set; }
        
        public ICollection<ProductSize>? ProductSizes { get; set; }

    }
}
