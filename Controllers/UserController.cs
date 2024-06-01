using AutoMapper;
using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.Logging;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
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
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    public UserController(ILogger<UserController> logger, AppDbContext db, IMapper mapper)
    {
        _logger = logger;
        _db = db;
        _mapper = mapper;
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
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        // _logger.LogInformation("Getting All Users");
        // _logger.Log("Getting All Users"," ");
        IEnumerable<User> users = await _db.Users.ToListAsync();
        return Ok(_mapper.Map<List<UserDto>>(users));
    }
    [HttpGet("{id:int}",Name="GetOneUser")] //name is to explicitly call it in post 
    // [ProducesResponseType(200,Type = typeof(UserDto))]
    // [ProducesResponseType(404)]
    // [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> GetOneUser(int id)
    {
        if (id == 0)
        {
            _logger.LogError("Get User Error with Id : " + id);
            // _logger.Log("Get User Error with Id : " + id,"error");
            return BadRequest();
        }

        var user =await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        if(user==null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [FromBody] means the body of http req
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateDto createDto)
    {
        //this is unnecessary unless we comment [ApiController]
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        
        //custom Error
        if (await _db.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError","User already Exists!");
            return BadRequest(ModelState);
        }
        
        if (createDto == null)
        {
            return BadRequest(createDto);
        }

        // if (userDto.Id > 0)
        // {
        //     return StatusCode(StatusCodes.Status500InternalServerError);
        // }
        User model = _mapper.Map<User>(createDto);
        
        await _db.Users.AddAsync(model);
        await _db.SaveChangesAsync();
        //this is fine but sometimes front want the end points, if that's the case we add explicit func to getbyid methode
        // return Ok(userDto);
        // we get the route in the http response header 
        return CreatedAtRoute("GetOneUser",new { id=model.Id }, model);
    }

    [HttpDelete("{id:int}", Name = "DeleteUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}",Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUser(int id,[FromBody] UserUpdateDto updateDto)
    {
        if (updateDto == null || id != updateDto.Id)
        {
            return BadRequest();
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
        User model = _mapper.Map<User>(updateDto);
        _db.Users.Update(model);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdatePartialUser(int id, JsonPatchDocument<UserUpdateDto> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }
        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        UserUpdateDto userDto = _mapper.Map<UserUpdateDto>(user);
        patchDto.ApplyTo(userDto, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        User model = _mapper.Map<User>(userDto);
        _db.Users.Update(model);
        await _db.SaveChangesAsync();
        return NoContent();
    }
     
}