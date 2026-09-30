using System.ComponentModel.DataAnnotations;

namespace csm_backend.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Please enter your username")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters long.")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
    public class LoginDto
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; } = "";
    }
}