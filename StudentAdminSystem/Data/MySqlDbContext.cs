using Microsoft.EntityFrameworkCore;
using StudentAdminSystem.Models;

namespace StudentAdminSystem.Data
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions options) : base(options)
        {
        }

        // FIXED: Added generic type <Student>
        public DbSet<Student> Students { get; set; } = null!;

        // FIXED: Added generic type <Admin>
        public DbSet<Admin> Admins { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FIXED: Added generic type <Student>
            modelBuilder.Entity<Student>().HasKey(s => s.StudentId);
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // FIXED: Added generic type <Admin>
            modelBuilder.Entity<Admin>().HasKey(a => a.AdminId);
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Email)
                .IsUnique();
        }
    }
}