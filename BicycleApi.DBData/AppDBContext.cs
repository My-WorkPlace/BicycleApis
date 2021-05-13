using BicycleApi.DBData.Entities;
using Microsoft.EntityFrameworkCore;

namespace BicycleApi.DBData
{
	public class AppDBContext : DbContext
	{ 
		public DbSet<Detail> Details { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Country> Countries { get; set; }
		public AppDBContext(DbContextOptions options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Brand>().HasIndex(u => u.Name)
				.IsUnique();

			builder.Entity<Country>().HasIndex(u => u.Name)
				.IsUnique();

			base.OnModelCreating(builder);
		}
	}
}
