using Microsoft.EntityFrameworkCore;
using MyBookApp.Models;

namespace BookApp.Data;

public class BookDbContext : DbContext {
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

    public DbSet<BookModel> Books { get; set; }
}