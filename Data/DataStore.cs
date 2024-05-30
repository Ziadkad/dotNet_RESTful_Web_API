using dotNet_RESTful_Web_API.models.Dto;

namespace dotNet_RESTful_Web_API.Data;

public static class DataStore
{
    public static List<UserDto> UserList = new List<UserDto> 
    {
        new UserDto { Id = 1, Name = "u1" , Age = 8 , Disability = true},
        new UserDto { Id = 2, Name = "u2" , Age = 20 , Disability = false},
        new UserDto { Id = 3, Name = "u3" , Age = 25 , Disability = false}
    };

}