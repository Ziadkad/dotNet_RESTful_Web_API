

using System.Net;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_RESTful_Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalUserController : ControllerBase
{
    private readonly ILocalUserRepository _localUser;
    protected ApiResponse _response;

    public LocalUserController(ILocalUserRepository localUser)
    {
        _localUser = localUser;
        this._response = new();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _localUser.Login(model);
        if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username or Password is Incorrect");
            return BadRequest(_response);
        }
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = loginResponse;
        return Ok(_response);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Registe([FromBody] RegistrationRequestDto model)
    {
        bool isUnique = _localUser.IsUniqueUser(model.Username);
        if (!isUnique)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username Already exists!");
            return BadRequest(_response);
        }

        var user = await _localUser.Register(model);
        if (user == null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Error Registering");
            return BadRequest(_response);
        } 
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        return Ok(_response);
    }
}