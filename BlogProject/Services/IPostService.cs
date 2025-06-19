using BlogProject.DTOs;
using BlogProject.Models;

namespace BlogProject.Services
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(PostCreateDto dto, int userId);
        Task<Post?> GetPostByIdAsync(int id);
        Task<Post?> UpdatePostAsync(int id, PostCreateDto dto, int userId);
        Task<bool> DeletePostAsync(int id, int userId);
        Task<List<Post>> GetPostsByUserAsync(int userId);
    }
}
