namespace BlogProject.DTOs
{
    public class CommentCreateDto
    {
        public string Text { get; set; } = string.Empty;
        public int PostId { get; set; }
    }
}
