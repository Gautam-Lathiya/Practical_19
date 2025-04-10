using Microsoft.EntityFrameworkCore;
using Practical_17.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Practical_17.ViewModels;

namespace Practical_17.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "User" }
            );
        }
        public DbSet<Practical_17.ViewModels.StudentViewModel> StudentViewModel { get; set; } = default!;
    }

}
