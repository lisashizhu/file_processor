using System;
using Microsoft.EntityFrameworkCore;

namespace FileProcessor.Data
{
	public class DatabaseContext : DbContext	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
		}

		public DbSet<STORE_ORDER> StoreOrder { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<STORE_ORDER>().HasKey(x => new { x.ID });
        }
	}
}

