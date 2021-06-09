using Amazon.Scrapper.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Amazon.Scrapper.EF
{
	public class AmazonScrapperContext : DbContext
	{
		public AmazonScrapperContext(DbContextOptions<AmazonScrapperContext> options) : base(options)
		{ 
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new Configurations.Product());
			builder.ApplyConfiguration(new Configurations.Review());
		}
	}
}
