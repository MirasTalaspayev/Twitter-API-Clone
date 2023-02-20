using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TwitterAPIClone.Models;

namespace TwitterAPIClone.Data;
[Authorize(Roles = Constants.ADMIN_ROLE)]
public class AdminDbContext : DbContext
{
    public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
    {
    }

    public DbSet<User> Admins { get; set; }
    public DbSet<User> AdminRequests { get; set; }
}

