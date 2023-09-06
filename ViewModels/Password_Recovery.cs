using System.ComponentModel.DataAnnotations;

namespace AlphaBeratung.ViewModels
{
    public class Password_Recovery
    {
        [Required(ErrorMessage = "Please Enter Your Email")]
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
    }
}
