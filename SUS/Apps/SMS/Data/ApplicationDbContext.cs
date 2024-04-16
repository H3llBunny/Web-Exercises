
using Microsoft.EntityFrameworkCore;

namespace SMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SMS;Integrated Security=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(x => x.UserId);

            modelBuilder.Entity<Product>()
                .Property(x => x.CartId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
