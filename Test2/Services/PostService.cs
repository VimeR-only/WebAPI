using AutoMapper;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Models;

public class PostService : IPostService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PostService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        var posts = await _context.Posts.ToListAsync();
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<IEnumerable<PostDto>> GetByUserIdAsync(int userId)
    {
        var posts = await _context.Posts
            .Where(p => p.UserId == userId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto> CreateAsync(PostCreateDto dto)
    {
        var post = _mapper.Map<Post>(dto);
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return _mapper.Map<PostDto>(post);
    }
}