using Amazon.Scrapper.Entities;

namespace Amazon.Scrapper.EF.Repositories
{
	public class ProductRepository : EfCoreRepository<Product, AmazonScrapperContext>
	{
		public ProductRepository(AmazonScrapperContext context) : base(context)
		{ }
	}
}
