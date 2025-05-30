using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Models;

namespace Test2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllAsync());
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(await _postService.GetByUserIdAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreateDto dto)
        {
            var post = await _postService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = post.Id }, post);
        }
    }
}