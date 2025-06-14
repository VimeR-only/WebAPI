using DTOs;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllAsync();
    Task<IEnumerable<PostDto>> GetByUserIdAsync(int userId);
    Task<PostDto> CreateAsync(PostCreateDto dto);
}