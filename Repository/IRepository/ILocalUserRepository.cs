using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;

namespace dotNet_RESTful_Web_API.Repository.IRepository;

public interface ILocalUserRepository
{
    bool IsUniqueUser(string username);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto);
}