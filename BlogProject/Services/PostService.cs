using BlogProject.Data;
using BlogProject.DTOs;
using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogProject.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _db;

        public PostService(AppDbContext db)
        {
            _db = db;
        }

        public async Task SeedPostsAsync(int numberOfPosts, int userId)
        {
            for (int i = 1; i <= numberOfPosts; i++)
            {
                var post = new Post
                {
                    Title = $"Test Post #{i}",
                    Content = $"This is content for post #{i}",
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow.AddMinutes(-i) // Щоб різні дати
                };

                Console.WriteLine($"Post {i} create for content {i}");

                _db.Posts.Add(post);
            }
            await _db.SaveChangesAsync();
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

        public async Task<(List<PostWithAuthorDto> Posts, int TotalCount)> GetPagedPostsAsync(int pageNumber, int pageSize)
        {
            var query = _db.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .AsQueryable();

            int totalCount = await query.CountAsync();

            var posts = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PostWithAuthorDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    AuthorName = p.User.Username
                })
                .ToListAsync();

            return (posts, totalCount);
        }

        public async Task<Post> CreateWithImage(PostWithImageDto dto, int userId)
        {
            string? imagePath = null;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder); // на всякий випадок

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = $"/uploads/{uniqueFileName}";
            }

            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = userId,
                ImagePath = imagePath
            };

            _db.Posts.Add(post);
            await _db.SaveChangesAsync();

            return post;
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            var posts = await _db.Posts
                .Include(p => p.User)
                .ToListAsync();

            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                ImageUrl = string.IsNullOrEmpty(p.ImagePath) ? null : $"{p.ImagePath}",
                AuthorName = p.User.Username
            }).ToList();

            return postDtos;
        }
    }
}
