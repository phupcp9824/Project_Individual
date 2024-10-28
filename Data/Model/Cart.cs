using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Cart
    {
        [Key]
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string? Status { get; set; } //"Active" nếu giỏ hàng đang được dùng, hoặc "CheckedOut" nếu giỏ hàng đã được xử lý thanh toán
        public double? TotalAmount { get; set; }
        public int? UserId { get; set; } // Ensure each Cart just belongs to to User
        public User? User { get; set; }
        public ICollection<OrderDetail>? orderDetails { get; set; }
    }
}
