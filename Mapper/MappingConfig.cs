using AutoMapper;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;

namespace dotNet_RESTful_Web_API.Mapper;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
    }
}