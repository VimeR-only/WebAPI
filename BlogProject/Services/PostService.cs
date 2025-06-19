using BlogProject.Data;
using BlogProject.DTOs;
using BlogProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _db;

        public PostService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Post> CreatePostAsync(PostCreateDto dto, int userId)
        {
            var post = new Post 
            { 
                Title = dto.Title,
                Content = dto.Content,
                UserId = userId
            };

            _db.Posts.Add(post);

            await _db.SaveChangesAsync();

            return post;
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _db.Posts.FindAsync(id);
        }

        public async Task<Post?> UpdatePostAsync(int id, PostCreateDto dto, int userId)
        {
            var post = await _db.Posts.FindAsync(id);

            if (post == null || post.UserId != userId) return null;

            post.Title = dto.Title;
            post.Content = dto.Content;

            await _db.SaveChangesAsync();

            return post;
        }

        public async Task<bool> DeletePostAsync(int id, int userId)
        {
            var post = await _db.Posts.FindAsync(id);

            if (post == null || post.UserId != userId) return false;

            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<Post>> GetPostsByUserAsync(int userId)
        {
            return await _db.Posts.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
