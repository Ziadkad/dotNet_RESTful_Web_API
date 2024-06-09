using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace dotNet_RESTful_Web_API.Repository;

public class LocalUserRepository : ILocalUserRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private string secretKey;
    public LocalUserRepository(AppDbContext db, IMapper mapper,IConfiguration configuration)
    {
        _db = db;
        _mapper = mapper;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
    }

    public bool IsUniqueUser(string username)
    {
        var user = _db.LocalUsers.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return true;
        }
        return false;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _db.LocalUsers.FirstOrDefaultAsync(u => u.Username.ToLower() == loginRequestDto.Username.ToLower() && u.Password == loginRequestDto.Password);
        if (user == null)
        {
            return new LoginResponseDto()
            {
                Token = "",
                User = null
            }; 
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
            }),
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = new( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            Token = tokenHandler.WriteToken(token),
            User = user
        };
        return loginResponseDto;
    }

    public async Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto)
    {
        LocalUser user = _mapper.Map<LocalUser>(registrationRequestDto);
        _db.LocalUsers.Add(user);
        await _db.SaveChangesAsync();
        user.Password = "";
        return user;
    }
}