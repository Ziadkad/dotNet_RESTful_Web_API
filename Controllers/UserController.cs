﻿using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.Logging;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_RESTful_Web_API.Controllers;
[Route("api/[controller]")]
[ApiController] // Also gives validations see UserDto.cs
public class UserController : ControllerBase
{
    // integrated logger
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _db;
    
    public UserController(ILogger<UserController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
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
    public ActionResult<IEnumerable<UserDto>> GetUsers()
    {
        // _logger.LogInformation("Getting All Users");
        // _logger.Log("Getting All Users"," ");
        return Ok(_db.Users);
    }
    [HttpGet("{id:int}",Name="GetOneUser")] //name is to explicitly call it in post 
    // [ProducesResponseType(200,Type = typeof(UserDto))]
    // [ProducesResponseType(404)]
    // [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserDto> GetOneUser(int id)
    {
        if (id == 0)
        {
            _logger.LogError("Get User Error with Id : " + id);
            // _logger.Log("Get User Error with Id : " + id,"error");
            return BadRequest();
        }

        var user = _db.Users.FirstOrDefault(u => u.Id == id);
        if(user==null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [FromBody] means the body of http req
    public ActionResult<UserDto> CreateUser([FromBody] UserDto userDto)
    {
        //this is unnecessary unless we comment [ApiController]
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        
        //custom Error
        if (_db.Users.FirstOrDefault(u => u.Name.ToLower() == userDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError","User already Exists!");
            return BadRequest(ModelState);
        }
        
        if (userDto == null)
        {
            return BadRequest(userDto);
        }

        if (userDto.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        User model = new()
        {
            Id = userDto.Id,
            Age = userDto.Age,
            Disability = userDto.Disability,
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            ImageUrl = userDto.ImageUrl
        };
        _db.Users.Add(model);
        _db.SaveChanges();
        //this is fine but sometimes front want the end points, if that's the case we add explicit func to getbyid methode
        // return Ok(userDto);
        // we get the route in the http response header 
        return CreatedAtRoute("GetOneUser",new { id=userDto.Id }, userDto);
    }

    [HttpDelete("{id:int}", Name = "DeleteUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteUser(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var user = _db.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        _db.Users.Remove(user);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id:int}",Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateUser(int id,[FromBody] UserDto userDto)
    {
        if (userDto == null || id != userDto.Id)
        {
            return BadRequest();
        }
        // var user = DataStore.UserList.FirstOrDefault(u => u.Id == id);
        // user.Name = userDto.Name;
        // user.Age = userDto.Age;
        // user.Disability = userDto.Disability;
        User model = new()
        {
            Id = userDto.Id,
            Age = userDto.Age,
            Disability = userDto.Disability,
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            ImageUrl = userDto.ImageUrl
        };
        _db.Users.Update(model);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdatePartialUser(int id, JsonPatchDocument<UserDto> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }
        var user = _db.Users.FirstOrDefault(u => u.Id == id);
        UserDto userDto = new()
        {
            Id = user.Id,
            Age = user.Age,
            Disability = user.Disability,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            ImageUrl = user.ImageUrl
        };
        if (user == null)
        {
            return NotFound();
        }
        patchDto.ApplyTo(userDto, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        User model = new()
        {
            Id = userDto.Id,
            Age = userDto.Age,
            Disability = userDto.Disability,
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            ImageUrl = userDto.ImageUrl
        };
        _db.Users.Update(model);
        _db.SaveChanges();
        return NoContent();
    }
     
}