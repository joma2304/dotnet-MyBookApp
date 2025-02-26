using Microsoft.EntityFrameworkCore;
using MyBookApp.Models;

namespace MyBookApp.Data;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

    public DbSet<BookModel> Books { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<LoanModel> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Relationer i databasen
        // En bok kan ha flera lån
        modelBuilder.Entity<LoanModel>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Restrict); // bok går inte att ta bort om den är utlånad

        // En användare kan ha flera lån
        modelBuilder.Entity<LoanModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.SetNull); // Om en användare tas bort, behåll lånet men nollställ UserId
    }

}