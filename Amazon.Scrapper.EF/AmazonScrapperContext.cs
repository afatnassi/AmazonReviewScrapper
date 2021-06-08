using Amazon.Scrapper.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Amazon.Scrapper.EF
{
	public class AmazonScrapperContext : DbContext
	{
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new Configurations.Product());
			builder.ApplyConfiguration(new Configurations.Review());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite(@"Data Source=C:\blogging.db");
	}
}
