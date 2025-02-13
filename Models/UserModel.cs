using System.ComponentModel.DataAnnotations;

namespace MyBookApp.Models;

public class UserModel
{
    public int Id { get; set; }

    [Required]
    public string? FristName { get; set; }
    [Required]
    public string? Lastname { get; set; }

    public ICollection<BookModel>? Books { get; set; }


}