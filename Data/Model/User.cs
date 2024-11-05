using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Username must from 3 - 25 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Username must from 3 - 25 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Username must from 6 - 25 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(03|09\d{8})$", ErrorMessage = "Phone number must start with 03 or 09 and be followed by 8 digits.")]
        public int Phone {  get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [StringLength(255, ErrorMessage = "Address must be under 256 characters")]
        public string Address { get; set; }

        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<Order>? orders { get; set; }
        public ICollection<Invoice>? invoices { get; set; }


    }
}
