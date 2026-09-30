using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace csm_backend.Models
{
    public enum UserRole
    {
        Admin,
        Author
    }

    [Index(nameof(Username), IsUnique = true)]
    public class UserModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter your username")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters long.")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        public UserRole UserRole { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<PostModel> Posts { get; set; } = new List<PostModel>(); 
    }
}