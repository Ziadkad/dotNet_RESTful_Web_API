using AutoMapper;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;

namespace dotNet_RESTful_Web_API.Mapper;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();

        CreateMap<UserNumber, UserNumberDto>().ReverseMap();
        CreateMap<UserNumber, UserNumberCreateDto>().ReverseMap();
        CreateMap<UserNumber, UserNumberUpdateDto>().ReverseMap();

        CreateMap<LocalUser, RegistrationRequestDto>().ReverseMap();
        CreateMap<LocalUser, LoginRequestDto>().ReverseMap();
        
    }
}