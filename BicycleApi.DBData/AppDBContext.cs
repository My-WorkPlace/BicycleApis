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
	}
}
