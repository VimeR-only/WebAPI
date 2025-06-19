using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public int PostId { get; set; }

        public Post? Post { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
