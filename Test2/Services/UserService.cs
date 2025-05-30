using AutoMapper;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Models;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReadDto>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserReadDto>>(users);
    }

    public async Task<UserReadDto?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? null : _mapper.Map<UserReadDto>(user);
    }

    public async Task<UserReadDto> CreateAsync(UserCreateDto dto)
    {
        var users = _mapper.Map<User>(dto);
        _context.Users.Add(users);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserReadDto>(users);
    }

    public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _mapper.Map(dto, user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}