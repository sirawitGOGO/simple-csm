using System.ComponentModel.DataAnnotations;

namespace csm_backend.Models
{
    public enum PostStatus
    {
        Pending,
        Publish,
        Failed
    }
    public class PostModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter your title")]
        [StringLength(50, ErrorMessage = "The title must not exceed 50 characters long.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Please enter your content")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "The content must be between 10 and 200 characters long.")]
        public string Content { get; set; } = "";

        public PostStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserModel? User { get; set; }

    }
}