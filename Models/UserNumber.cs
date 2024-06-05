using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNet_RESTful_Web_API.models;

public class UserNumber
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int UserNo { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    public string? Details { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}