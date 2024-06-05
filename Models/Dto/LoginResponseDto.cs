namespace dotNet_RESTful_Web_API.models.Dto;

public class LoginResponseDto
{
    public LocalUser User { get; set; }
    public string Token { get; set; }
}