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

        CreateMap<User, UserReadDto>()
            .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts));
        
        CreateMap<Post, PostInUserDto>();
    }
}