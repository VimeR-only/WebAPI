using Microsoft.AspNetCore.Mvc;
using BlogProject.DTOs;
using BlogProject.Models;
using BlogProject.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, PostCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var post = _postService.UpdatePostAsync(id, dto, userId);

            if (post == null) return NotFound();

            return Ok(post);
        }

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

    }
}
