using System.Net;
using Asp.Versioning;
using AutoMapper;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_RESTful_Web_API.Controllers;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class UserNumbev2Controller : ControllerBase
{
    private readonly IUserNumberRepository _dbUserNumber;
    private readonly IUserRepository _dbUser;
    private readonly IMapper _mapper;
    protected ApiResponse _response;
    public UserNumbev2Controller(IUserNumberRepository dbUserNumber, IMapper mapper,IUserRepository dbUser)
    {
        _dbUserNumber = dbUserNumber;
        _mapper = mapper;
        _response = new();
        _dbUser = dbUser;
    }
    [HttpGet]
    [MapToApiVersion(1.0)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ApiResponse>>> GetUsersNumbers()
    {
        try
        {
            IEnumerable<UserNumber>? userNumbers = await _dbUserNumber.GetAllAsync();
            _response.Result = _mapper.Map<List<UserNumberDto>>(userNumbers);
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
    
    [HttpGet]
    [MapToApiVersion(2.0)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public  ActionResult<ApiResponse> GetUsersNumbers2()
    {
            _response.Result = Ok("hihihihih");
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
    }
    
    
    [HttpGet("{userNo:int}",Name="GetOneUserNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> GetOneUserNumber(int userNo)
    {
        try
        {
            if (userNo == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var userNumber =await _dbUserNumber.GetAsync(u=>u.UserNo == userNo);
            if(userNumber==null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<UserNumberDto>(userNumber);
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

    
    public async Task<ActionResult<ApiResponse>> CreateUser([FromBody] UserNumberCreateDto createDto)
    {
        try
        {
            if (createDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            if (await _dbUser.GetAsync(u => u.Id == createDto.UserId)==null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            if (await _dbUserNumber.GetAsync(u => u.UserNo == createDto.UserNo) != null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            
            UserNumber userNumber = _mapper.Map<UserNumber>(createDto);

            await _dbUserNumber.CreateAsync(userNumber);
            _response.Result = _mapper.Map<UserNumberDto>(userNumber);
            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            return CreatedAtRoute("GetOneUserNumber",new { UserNo=userNumber.UserNo }, _response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
            return Ok(_response);
        }
    }
    [HttpDelete("{userNo:int}", Name = "DeleteUserNumber")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> DeleteUserNumber(int userNo)
    {
        try
        {
            if (userNo == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var userNumber = await _dbUserNumber.GetAsync(u => u.UserNo == userNo);
            if (userNumber == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            await _dbUserNumber.RemoveAsync(userNumber);
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
    [HttpPut("{userNo:int}",Name = "UpdateUserNumber")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> UpdateUserNumber(int userNo,[FromBody] UserNumberUpdateDto updateDto)
    {
        try
        {
            if (updateDto == null || userNo != updateDto.UserNo)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            if (await _dbUser.GetAsync(u => u.Id == updateDto.UserId)==null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            UserNumber UserNumber = _mapper.Map<UserNumber>(updateDto);
            
            await _dbUserNumber.UpdateAsync(UserNumber);
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
}