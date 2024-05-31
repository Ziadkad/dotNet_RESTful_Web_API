using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNet_RESTful_Web_API.models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password  { get; set; }
    public int Age { get; set; }
    public bool Disability { get; set; }
    public string ImageUrl { get; set; } 
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}