using System.ComponentModel.DataAnnotations;

namespace AuthProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 6, ErrorMessage = "field must be atleast 6 characters")]
        public string? Password { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 6, ErrorMessage = "field must be atleast 6 characters")]
        public string? ConfirmPassword { get; set; }
          
    }
}
