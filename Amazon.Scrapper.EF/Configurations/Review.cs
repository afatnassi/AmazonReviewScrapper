using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.EF.Configurations
{
	class Review : IEntityTypeConfiguration<Entities.Review>
	{
		public void Configure(EntityTypeBuilder<Entities.Review> builder)
		{

			builder
				.HasKey(r => r.Id);

			builder
				.HasOne(r => r.Product)
				.WithMany()
				.HasForeignKey(r => r.ProductId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
