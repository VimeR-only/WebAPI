using System.ComponentModel.DataAnnotations;

namespace BlogProject.DTOs
{
    public class PostWithAuthorDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public string AuthorName { get; set; } = string.Empty;
    }
}
