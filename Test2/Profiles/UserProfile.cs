using AutoMapper;
using Test2.Models;
using DTOs;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReadDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}