using Microsoft.EntityFrameworkCore;
using MovieApp.Models;

namespace MovieApp;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Character> Characters { get; set; }
}