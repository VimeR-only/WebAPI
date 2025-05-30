namespace Test2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsActive { get; set; } = false;
        public ICollection<Post> Posts { get; set; }
    }
}