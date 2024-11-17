using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public ICollection<Product>? products { get; set; }
    }
}
