using System.ComponentModel.DataAnnotations;

namespace dotNet_RESTful_Web_API.models.Dto;

public class UserUpdateDto
{
    [Required]
    public int Id { get; set; }
    [Required] //validation 
    [MaxLength(30)] //validation 
    public string Name { get; set; } = string.Empty;
    [Required]
    public int Age { get; set; }
    [Required]
    public bool Disability { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password  { get; set; }
    public string ImageUrl { get; set; } 
}