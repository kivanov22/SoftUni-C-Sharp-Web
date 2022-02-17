using CarShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-M28UK1S\\SQLEXPRESS;Database=CarShop;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Issue> Issues { get; set; }

        internal object Where()
        {
            throw new NotImplementedException();
        }
    }
}
