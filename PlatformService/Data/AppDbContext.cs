using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public class AppDbContext : DbContext
{
    public DbSet<Platform> Platforms => Set<Platform>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
