using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_RESTful_Web_API.Controllers;
[Route("api/[controller]")]
[ApiController] // Also gives validations see UserDto.cs
public class UserController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<UserDto>> GetUsers()
    {
        return Ok(DataStore.UserList);
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
            return BadRequest();
        }

        var user = DataStore.UserList.FirstOrDefault(u => u.Id == id);
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
        if (DataStore.UserList.FirstOrDefault(u => u.Name.ToLower() == userDto.Name.ToLower()) != null)
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
        //incrementingID
        userDto.Id = DataStore.UserList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        DataStore.UserList.Add(userDto);
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
        var user = DataStore.UserList.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        DataStore.UserList.Remove(user);
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
        var user = DataStore.UserList.FirstOrDefault(u => u.Id == id);
        user.Name = userDto.Name;
        user.Age = userDto.Age;
        user.Disability = userDto.Disability;
        return NoContent();
    }
}