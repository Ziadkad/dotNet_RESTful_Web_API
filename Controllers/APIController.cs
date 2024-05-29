using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_RESTful_Web_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetUsers()
    {
        return Ok(DataStore.UserList);
    }
    [HttpGet("{id:int}")]
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
}