using BlogProject.DTOs;
using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Services
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(PostCreateDto dto, int userId);
        Task<Post> CreateWithImage(PostWithImageDto dto, int userId);
        Task<Post?> GetPostByIdAsync(int id);
        Task<List<PostDto>> GetAllPostsAsync();
        Task<Post?> UpdatePostAsync(int id, PostCreateDto dto, int userId);
        Task<bool> DeletePostAsync(int id, int userId);
        Task<List<Post>> GetPostsByUserAsync(int userId);
        Task<(List<PostWithAuthorDto> Posts, int TotalCount)> GetPagedPostsAsync(int pageNumber, int pageSize);
        Task SeedPostsAsync(int numberOfPosts, int userId);
    }
}
