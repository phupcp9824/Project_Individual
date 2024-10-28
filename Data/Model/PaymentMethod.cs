using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
