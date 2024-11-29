using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Role 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Name must from 6 - 25 characters")]
        public string Name { get; set; }
            
        [StringLength(255, ErrorMessage = "Description must be under 256 characters")]
        public string? Description { get; set; }

        public ICollection<User>? users { get; set; }
    }
}
