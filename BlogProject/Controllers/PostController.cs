using Microsoft.AspNetCore.Mvc;
using BlogProject.DTOs;
using BlogProject.Models;
using BlogProject.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(PostCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var post = await _postService.CreatePostAsync(dto, userId);

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpGet]
        public ActionResult<Post> GetPostById(int id)
        {
            var post = _postService.GetPostByIdAsync(id);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var (posts, totalCount) = await _postService.GetPagedPostsAsync(pageNumber, pageSize);

            return Ok(new
            {
                TotalItems = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Items = posts
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, PostCreateDto dto)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var post = await _postService.GetPostByIdAsync(id);

            if (post.UserId != userId && userRole != "Admin")
                return Unauthorized();

            var newPost = _postService.UpdatePostAsync(id, dto, userId);

            if (newPost == null) return NotFound();

            return Ok(newPost);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var success = await _postService.DeletePostAsync(id, userId);

            if (!success) return NotFound();

            return Ok(success);
        }

        [HttpGet("my")]
        public async Task<ActionResult<List<Post>>> GetMyPosts()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var posts = await _postService.GetPostByIdAsync(userId);

            return Ok(posts);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<PostDto>>> GetAll()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            await _postService.SeedPostsAsync(50, 1);
            return Ok("Seed complete.");
        }

        [Authorize]
        [HttpPost("with-image")]
        public async Task<IActionResult> CreateWithImage([FromForm] PostWithImageDto dto)
        {
           var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

           var post = await _postService.CreateWithImage(dto, userId);

           return Ok(post);
        }

    }
}
