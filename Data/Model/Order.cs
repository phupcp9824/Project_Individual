using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; } 
        public int? UserId { get; set; }
        public int? PaymentMethodId { get; set; }  
        public int? PromotionId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Order name cannot exceed 50 characters.")]
        public string OrderName { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total must be a positive value or zero.")]
        public double Total { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ICollection<OrderDetail>? orderDetails { get; set; }
        public User? User { get; set; }
        public Invoice? Invoice { get; set; }
        public PaymentMethod? paymentMethods { get; set; }
        public Promotion? Promotion { get; set; }
    }
}
