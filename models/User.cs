namespace dotNet_RESTful_Web_API.models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string email { get; set; }
    public string password  { get; set; }
    public int Age { get; set; }
    public bool Disability { get; set; }
    public string ImageUrl { get; set; } 
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}