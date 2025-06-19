using Microsoft.AspNetCore.Http;

namespace BlogProject.DTOs
{
    public class PostWithImageDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
    }
}
