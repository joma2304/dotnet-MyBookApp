using System.ComponentModel.DataAnnotations;

namespace MyBookApp.Models;

public class BookModel
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Author { get; set; }

    public string? Genre { get; set; }
    public int Pages { get; set; }


    // Koppling till Loans 
    public ICollection<LoanModel>? Loans { get; set; }
}
