using System.ComponentModel.DataAnnotations;

namespace dotNet_RESTful_Web_API.models.Dto;

public class UserNumberUpdateDto
{
    [Required]
    public int UserNo { get; set; }
    [Required]
    public int UserId { get; set; }
    
    public string? Details { get; set; }
}