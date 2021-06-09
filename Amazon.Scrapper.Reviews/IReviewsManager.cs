using Amazon.Scrapper.Entities;
using System.Collections.Generic;

namespace Amazon.Scrapper.Reviews
{
	public interface IReviewManager
	{
		List<Review> GetReviews();

		List<Review> GetReviewByASIN(string ASIN);
	}
}
