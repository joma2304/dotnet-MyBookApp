using System.ComponentModel.DataAnnotations;

namespace MyBookApp.Models;

public class UserModel
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    // Koppling böcker
    public ICollection<BookModel>? Books { get; set; }

    // koppling lån
    public ICollection<LoanModel>? Loans { get; set; } 
}