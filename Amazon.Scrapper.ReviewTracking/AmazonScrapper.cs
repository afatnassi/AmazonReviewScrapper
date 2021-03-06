using Amazon.Scrapper.Entities;
using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.ReviewTracking
{
	public class AmazonScrapper : IAmazonScrapper
	{
		private readonly string amazonUrl = @"https://www.amazon.com";

		public async Task<IDocument> GetPage(string url)
		{
			var config = Configuration.Default.WithDefaultLoader();
			var context = BrowsingContext.New(config);
			var document = await context.OpenAsync(url);
			return document;
		}

		public IEnumerable<Review> GetReviews(IDocument document)
		{
			IEnumerable<IElement> scrappedReviews = document.All.Where(x => x.ClassName == "a-section review aok-relative");
			
			foreach (var element in scrappedReviews)
			{
				var profileName = element.GetElementsByClassName("a-profile-name").FirstOrDefault()?.TextContent.Trim();
				var stars = element.GetElementsByClassName("review-rating").FirstOrDefault()?.FirstChild.TextContent.Trim().Substring(0, 1);
				var title = element.GetElementsByClassName("review-title").FirstOrDefault()?.TextContent.Trim();
				var content = element.GetElementsByClassName("review-text-content").FirstOrDefault()?.TextContent.Trim();
				var votes = element.GetElementsByClassName("cr-vote-text").FirstOrDefault()?.TextContent.Trim().Split(' ');

				Review review = new Review
				{
					Id = Guid.NewGuid(),
					ProfileName = profileName,
					Title = title,
					Content = content,
					Rating = int.Parse(stars),
					NumberOfVotes = (votes == null) ? 0 : ExtractVotes(votes)
				};
				yield return review;
			}
		}

		private static int ExtractVotes(string[] votes)
		{
			if (votes[0].ToLower() == "one")
				return 1;
			else
			{ return int.Parse(votes[0]); }
		}

		public async Task<List<Review>> ScrapReviewWebsite(string url)
		{
			List<Review> results = new List<Review>();

			while (!String.IsNullOrEmpty(url))
			{
				IDocument document = await GetPage(url);

				IEnumerable<Review> productReviews = GetReviews(document);
				results.AddRange(productReviews);

				string nextPageUrl = "";
				var nextPageLink = document.All.Where(x => x.ClassName == "a-last").FirstOrDefault();
				if (nextPageLink != null)
				{
					nextPageUrl = amazonUrl + nextPageLink.Children.FirstOrDefault()?.GetAttribute("Href");
				}
				url = nextPageUrl;
			}
			return results;
		}
	}
}
