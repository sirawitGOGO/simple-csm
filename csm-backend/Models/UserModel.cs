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
        [StringLength(20, MinimumLength = 5, ErrorMessage = "The username must be between 5 and 20 characters long.")]
        public string Username { get; set; } = "";
        public UserRole Role { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<PostModel> Posts { get; set; } = new List<PostModel>(); 
    }
}