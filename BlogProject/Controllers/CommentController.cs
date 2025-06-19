using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogProject.DTOs;
using BlogProject.Services;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateComment(CommentCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var comment = await _commentService.CreateCommentAsync(dto, userId);

            return Ok(comment);
        }

        [AllowAnonymous]
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<List<object>>> GetCommentsForPost(int postId)
        {
            var comment = await _commentService.GetCommentsByPostIdAsync(postId);

            var result = comment.Select(c => new
            {
                c.Id,
                c.Text,
                c.UserId,
                Username = c.User?.Username
            });

            return Ok(result);
        }
    }
}
