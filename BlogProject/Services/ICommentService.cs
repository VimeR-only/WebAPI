using BlogProject.DTOs;
using BlogProject.Models;

namespace BlogProject.Services
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(CommentCreateDto dto, int userId);
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
    }
}
