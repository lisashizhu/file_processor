using System;
using Microsoft.EntityFrameworkCore;

namespace FileProcessor.Test
{
	public static class InMemoryDbBuilder
	{
		public static TDbContext CreateInstance<TDbContext>(string databaseName = null,
			Action<DbContextOptionsBuilder<TDbContext>> configure=null)
			where TDbContext : DbContext
		{
			databaseName ??= Guid.NewGuid().ToString();
			var optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
				.UseInMemoryDatabase(databaseName);
			configure?.Invoke(optionsBuilder);
			var options = optionsBuilder.Options;

			return (TDbContext)Activator.CreateInstance(typeof(TDbContext),options);
		}
	}
}

