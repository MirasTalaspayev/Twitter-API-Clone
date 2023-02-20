using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TwitterAPIClone.Models;

namespace TwitterAPIClone.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSecrets> UserSecrets { get; set; }
    public DbSet<Tweet> Tweets { get; set; }
}

