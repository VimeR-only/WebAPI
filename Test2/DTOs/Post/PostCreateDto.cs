namespace DTOs
{
    public class PostCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}