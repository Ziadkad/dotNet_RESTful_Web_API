using System.ComponentModel.DataAnnotations;

namespace dotNet_RESTful_Web_API.models.Dto;

public class UserDto
{
    public int Id { get; set; }
    [Required] //validation 
    [MaxLength(30)] //validation 
    public string Name { get; set; }
}