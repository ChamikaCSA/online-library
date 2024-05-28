using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<CD> CD { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
    }
}
