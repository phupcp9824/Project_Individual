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

        [Required(ErrorMessage = "Recipient name is required.")]
        [StringLength(100, ErrorMessage = "Recipient name must not exceed 100 characters.")]
        public string ShipName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Delivery address is required.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(03|09\d{8}$", ErrorMessage = "The phone number is not in the correct format")]
        public int ShipPhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^(Paid|Unpaid)$", ErrorMessage = "Payment status is invalid.")]
        public string PaymentStatus { get; set; }
        public double TotalAmount { get; set; }
        public User? User { get; set; }
        public Invoice? Invoice { get; set; }
        public PaymentMethod? paymentMethods { get; set; }
        public Promotion? Promotion { get; set; }
    }
}
