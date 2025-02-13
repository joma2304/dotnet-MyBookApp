using System.ComponentModel.DataAnnotations;
namespace MyBookApp.Models;

public class BookModel
{
    // Properties
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public int Pages { get; set; }
    public DateOnly? LoanDate { get; set; }
    public int? UserId { get; set; }
    public UserModel? User { get; set; } 

}

