using Microsoft.EntityFrameworkCore;
using PhDManager.Core.Models;

namespace PhDManager.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; init; }
        public DbSet<Thesis> Theses { get; init; }
    }
}
