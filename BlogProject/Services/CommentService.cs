using BlogProject.Data;
using BlogProject.DTOs;
using BlogProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _db;

        public CommentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Comment> CreateCommentAsync(CommentCreateDto dto, int userId)
        {
            var comment = new Comment
            {
                Text = dto.Text,
                PostId = dto.PostId,
                UserId = userId
            };

            _db.Comments.Add(comment);

            await _db.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _db.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}
