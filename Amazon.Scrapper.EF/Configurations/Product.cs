using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amazon.Scrapper.EF.Configurations
{
	public class Product : IEntityTypeConfiguration<Entities.Product>
	{
		public void Configure(EntityTypeBuilder<Entities.Product> builder)
		{
			builder
				.HasKey(p => p.Id);

			builder
				.HasIndex(p => p.ASIN)
				.IsUnique();
		}
	}
}
