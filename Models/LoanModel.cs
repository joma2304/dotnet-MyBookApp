using System.ComponentModel.DataAnnotations;

namespace MyBookApp.Models;

public class LoanModel
{
    public int Id { get; set; }

    [Required]
    public DateOnly LoanDate { get; set; }

    // Koppling till Book
    public int? BookId { get; set; }
    public BookModel? Book { get; set; } 

    // Koppling till User
    public int? UserId { get; set; }
    public UserModel? User { get; set; } 
}