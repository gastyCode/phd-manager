using Microsoft.EntityFrameworkCore;
using PhDManager.Core.Models;

namespace PhDManager.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; init; }
        public DbSet<Thesis> Theses { get; init; }
        public DbSet<Subject> Subjects { get; init; }
        public DbSet<StudyProgram> StudyPrograms { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Thesis>()
                .HasOne(t => t.Student)
                .WithOne(t => t.Thesis)
                .HasForeignKey<User>("StudentId");
        }
    }
}
