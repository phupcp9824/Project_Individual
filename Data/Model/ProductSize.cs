using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }
        
        public int? ProductId { get; set; }
        public int? SizeId { get; set; }

        public Product? Product { get; set; }
        public Size? Size { get; set; }


    }
}
