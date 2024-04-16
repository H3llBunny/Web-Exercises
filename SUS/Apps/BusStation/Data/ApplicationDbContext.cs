using Microsoft.EntityFrameworkCore;

namespace BusStation.Data
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

        public DbSet<Destination> Destinations { get; set; }

		public DbSet<Ticket> Tickets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=.;Database=BusStation;Integrated Security=true;TrustServerCertificate=true;");
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Ticket>()
				.Property(x => x.UserId)
				.IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
