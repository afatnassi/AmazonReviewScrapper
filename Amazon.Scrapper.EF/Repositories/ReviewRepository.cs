using Amazon.Scrapper.Entities;

namespace Amazon.Scrapper.EF.Repositories
{
	public class ReviewRepository : EfCoreRepository<Review, AmazonScrapperContext>
	{
		public ReviewRepository(AmazonScrapperContext context) : base(context)
		{ }
	}
}
