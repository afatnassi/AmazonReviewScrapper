using Amazon.Scrapper.Entities;
using AngleSharp.Dom;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amazon.Scrapper.ReviewTracking
{
	public interface IAmazonScrapper
	{
		Task<IDocument> GetPage(string url);

		IEnumerable<Review> GetReviews(IDocument document);

		Task<List<Review>> ScrapReviewWebsite(string url);
	}
}
