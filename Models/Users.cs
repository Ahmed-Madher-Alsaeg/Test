using System.ComponentModel.DataAnnotations;

namespace AlphaBeratung.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Email")]
        
        [Required(ErrorMessage ="Please Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Display(Name ="Password")]
        [MinLength(6, ErrorMessage = "Your Password must be at least 6 characters long")]
        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;


        [Required(ErrorMessage = "Please Confirm Your Password")]
        [Compare("Password", ErrorMessage = "Your Password & and Confirm Password must be the same")]
        public string ConfirmPassword { get; set; } = null!;
        
    }
}
