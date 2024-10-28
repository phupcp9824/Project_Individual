using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        public string Code { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percent must be between 0 and 100.")]
        public double? DiscountPercent { get; set; }
        public double? DiscountAmount {  get; set; }

        public ICollection<Order>? orders { get; set; }
    }
}
