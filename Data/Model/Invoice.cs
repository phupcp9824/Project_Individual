using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? UserId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalAmount { get; set; }
        public Order? Order { get; set; }
        public User? User { get; set; }
    }
}
