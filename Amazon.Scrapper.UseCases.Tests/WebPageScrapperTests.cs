using Amazon.Scrapper.Entities;
using AngleSharp;
using AngleSharp.Dom;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Amazon.Scrapper.UseCases.Tests
{
	[TestClass]
	public class WebPageScrapperTests
	{
		private IWebPageScrapper _webPageScrapper;
		private ServiceCollection _services;
		private IServiceProvider _serviceProvider;

		[TestInitialize]
		public void SetUp()
		{
			_services = new ServiceCollection();
			_services.AddTransient<IWebPageScrapper, WebPageScrapper>();

			_serviceProvider = _services.BuildServiceProvider();
			_webPageScrapper = _serviceProvider.GetService<IWebPageScrapper>();
		}

		[TestMethod]
		public void GetPage_With_Invalid_Url_Should_Return_Null()
		{
			var invalidUrl = "invalidUrl";
			_webPageScrapper.GetPage(invalidUrl).Result.Should().BeNull();
		}

		[TestMethod]
		public void GetPage_With_Valid_Url_Should_Not_Return_Null()
		{
			var validUrl = "https://www.amazon.com/product-reviews/B082XY23D5";
			_webPageScrapper.GetPage(validUrl).Result.Should().NotBeNull();
		}

		[TestMethod]
		public async Task GetReviews_With_Valid_Document_Should_Not_Return_Null()
		{
			var source = @"
<!DOCTYPE html>
<html lang=en>
<div id=""cm_cr-review_list"">
<div id=""R3PFFVSQGIS9J6"" data-hook=""review"" class=""a-section review aok-relative"">
<span class=""a-profile-name"">Ta</span>
<i data-hook=""review-star-rating"" class=""a-icon a-icon-star a-star-1 review-rating""><span class=""a-icon-alt"">1.0 out of 5 stars</span></i>
<a data-hook=""review-title"" class=""a-size-base a-link-normal review-title a-color-base review-title-content a-text-bold"">
<span> FM Radio still not active in the US unlocked version</ span >
</a>
<span data-hook=""review-body"" class=""a-size-base review-text review-text-content"">
<span>
	Despite Samsung's promises, and despite being readily available in the hardware, the FM Radio is not active in the US unlocked version's chip.
</span>
</span>
<div>
<span data-hook=""helpful-vote-statement"" class=""a-size-base a-color-tertiary cr-vote-text"">267 people found this helpful</span>
</div>
</div>
</div>
";
			IDocument document = await GetDocument(source);

			var reviews = _webPageScrapper.GetReviews(document);

			Review excpectedReview = new Review
			{
				ProfileName = "Ta",
				Title = "FM Radio still not active in the US unlocked version",
				Content = @"Despite Samsung's promises, and despite being readily available in the hardware, the FM Radio is not active in the US unlocked version's chip.",
				Rating = 1,
				NumberOfVotes = 267
			};


			reviews.Should().NotBeNull();
			reviews.First().ProfileName.Should().BeEquivalentTo(excpectedReview.ProfileName);
			reviews.First().Title.Should().BeEquivalentTo(excpectedReview.Title);
			reviews.First().Content.Should().BeEquivalentTo(excpectedReview.Content);
			reviews.First().Rating.Should().Be(excpectedReview.Rating);
			reviews.First().NumberOfVotes.Should().Be(excpectedReview.NumberOfVotes);
		}

		private static async Task<IDocument> GetDocument(string source)
		{
			var config = Configuration.Default.WithDefaultLoader();
			var context = BrowsingContext.New(config);
			var document = await context.OpenAsync(req => req.Content(source));
			return document;
		}
	}
}
