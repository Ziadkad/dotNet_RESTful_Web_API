using System.Net;
using AutoMapper;
using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.Logging;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotNet_RESTful_Web_API.Controllers;
[Route("api/[controller]")]
[ApiController] // Also gives validations see UserDto.cs
public class UserController : ControllerBase
{
    // integrated logger
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _dbUser;
    private readonly IMapper _mapper;
    protected ApiResponse _response;
    public UserController(ILogger<UserController> logger, IUserRepository dbUser, IMapper mapper)
    {
        _logger = logger;
        _dbUser = dbUser;
        _mapper = mapper;
        this._response = new();
    }
    // custom Logger
    // private readonly ILogging _logger;
    //
    // public UserController(ILogging logger)
    // {
    //     _logger = logger;
    // }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ApiResponse>>> GetUsers()
    {
        // _logger.LogInformation("Getting All Users");
        try
        {
            IEnumerable<User>? users = await _dbUser.GetAllAsync();
            _response.Result = _mapper.Map<List<UserDto>>(users);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
            
        }
        return Ok(_response);
    }
    [HttpGet("{id:int}",Name="GetOneUser")] //name is to explicitly call it in post 
    // [ProducesResponseType(200,Type = typeof(UserDto))]
    // [ProducesResponseType(404)]
    // [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> GetOneUser(int id)
    {
        try
        {
            if (id == 0)
            {
                // _logger.LogError("Get User Error with Id : " + id);
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var user =await _dbUser.GetAsync(u=>u.Id == id);
            if(user==null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<UserDto>(user);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
            
        }
        return Ok(_response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [FromBody] means the body of http req
    public async Task<ActionResult<ApiResponse>> CreateUser([FromBody] UserCreateDto createDto)
    {
        //this is unnecessary unless we comment [ApiController]
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        
        //custom Error
        try
        {
            if (await _dbUser.GetAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError","User already Exists!");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            // if (userDto.Id > 0)
            // {
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }
            User user = _mapper.Map<User>(createDto);

            await _dbUser.CreateAsync(user);
            //this is fine but sometimes front want the end points, if that's the case we add explicit func to getbyid methode
            // return Ok(userDto);
            // we get the route in the http response header 
        
            _response.Result = _mapper.Map<UserDto>(user);
            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            return CreatedAtRoute("GetOneUser",new { id=user.Id }, _response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
            return Ok(_response);
        }
    }

    [HttpDelete("{id:int}", Name = "DeleteUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> DeleteUser(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var user = await _dbUser.GetAsync(u => u.Id == id);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            await _dbUser.RemoveAsync(user);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
            return Ok(_response);
        }
    }

    [HttpPut("{id:int}",Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> UpdateUser(int id,[FromBody] UserUpdateDto updateDto)
    {
        try
        {
            if (updateDto == null || id != updateDto.Id)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            // var user = DataStore.UserList.FirstOrDefault(u => u.Id == id);
            // user.Name = userDto.Name;
            // user.Age = userDto.Age;
            // user.Disability = userDto.Disability;
            // User model = new()
            // {
            //     Id = updateDto.Id,
            //     Age = updateDto.Age,
            //     Disability = updateDto.Disability,
            //     Name = updateDto.Name,
            //     Email = updateDto.Email,
            //     Password = updateDto.Password,
            //     ImageUrl = updateDto.ImageUrl
            // };
            User user = _mapper.Map<User>(updateDto);
            await _dbUser.UpdateAsync(user);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
            return Ok(_response);
        }
        
    }

    // [HttpPatch("{id:int}", Name = "UpdatePartialUser")]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // public async Task<ActionResult<ApiResponse>> UpdatePartialUser(int id, JsonPatchDocument<UserUpdateDto> patchDto)
    // {
    //     if (patchDto == null || id == 0)
    //     {
    //         return BadRequest();
    //     }
    //     var user = await _dbUser.GetAsync(u => u.Id == id,tracked:false);
    //     if (user == null)
    //     {
    //         return NotFound();
    //     }
    //     UserUpdateDto userDto = _mapper.Map<UserUpdateDto>(user);
    //     patchDto.ApplyTo(userDto, ModelState);
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest();
    //     }
    //
    //     User model = _mapper.Map<User>(userDto);
    //     await _dbUser.UpdateAsync(model);
    //     return NoContent();
    // }
    //  
}