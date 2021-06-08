using Amazon.Scrapper.Entities;
using AngleSharp.Dom;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amazon.Scrapper.ReviewTracking
{
	public interface IWebPageScrapper
	{
		Task<IDocument> GetPage(string url);

		IEnumerable<Review> GetReviews(IDocument document);
	}
}
