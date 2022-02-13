namespace SMS.Data
{
    using Microsoft.EntityFrameworkCore;
    using SMS.Data.Models;

    // ReSharper disable once InconsistentNaming
    public class SMSDbContext : DbContext
    {
        public SMSDbContext()
        {
            
        }

        public DbSet<Cart> Carts { get; init; }

        public DbSet<Product> Products { get; init; }

        public DbSet<User> Users { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //'Cart.User' and 'User.Cart'
            // modelBuilder.Entity<Author>()
            //.HasOne(a => a.Biography)
            //.WithOne(b => b.Author)
            //.HasForeignKey<AuthorBiography>(b => b.AuthorRef);

            //modelBuilder.Entity<User>()
            //    .HasOne<Cart>(a => a.Cart)
            //    .WithOne(u => u.User)
            //    .HasForeignKey<Cart>(u => u.Cart);

            modelBuilder.Entity<Cart>()
                .HasOne<User>(u => u.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<User>(i => i.CartId);
        }
    }
}