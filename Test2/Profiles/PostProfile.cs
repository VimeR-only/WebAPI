using AutoMapper;
using Test2.Models;
using DTOs;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap<PostCreateDto, Post>();
        CreateMap<PostUpdateDto, Post>();
    }
}