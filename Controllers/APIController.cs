using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_RESTful_Web_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    [HttpGet]
    public IEnumerable<UserDto> GetVillas()
    {
        return new List<UserDto>
        {

        };
    }
}