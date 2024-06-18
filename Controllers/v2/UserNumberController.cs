using System.Net;
using Asp.Versioning;
using AutoMapper;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_RESTful_Web_API.Controllers.v2;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class UserNumberController : ControllerBase
{
    private readonly IUserNumberRepository _dbUserNumber;
    private readonly IUserRepository _dbUser;
    private readonly IMapper _mapper;
    protected ApiResponse _response;
    public UserNumberController(IUserNumberRepository dbUserNumber, IMapper mapper,IUserRepository dbUser)
    {
        _dbUserNumber = dbUserNumber;
        _mapper = mapper;
        _response = new();
        _dbUser = dbUser;
    }
   
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public  ActionResult<ApiResponse> GetUsersNumbers2()
    {
            _response.Result = Ok("hihihihih");
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
    }
    
}