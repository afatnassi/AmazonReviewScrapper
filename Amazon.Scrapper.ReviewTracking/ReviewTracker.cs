using Amazon.Scrapper.Entities;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.ReviewTracking
{
	public class ReviewTracker : IReviewTracker
	{
		private readonly string amazonProductUrl = @"https://www.amazon.com/product-reviews/";
		private IAmazonScrapper AmazonScrapper {get; set;}
		private IRepository<Review> ReviewRepo { get; set; }
		private IRepository<Product> ProductRepo { get; set; }
		private IEmailSender EmailSender { get; set; }

		public ReviewTracker(
			IAmazonScrapper amazonScrapper,
			IRepository<Review> reviewrepo,
			IRepository<Product> productRepo,
			IEmailSender emailSender)
		{
			AmazonScrapper = amazonScrapper;
			ReviewRepo = reviewrepo;
			EmailSender = emailSender;
			ProductRepo = productRepo;
		}

		public async Task CheckForUpdates(string url)
		{
			string ASIN = url.Split('/').Last();
			string mailTitle = "Product Reviews Update";
			string mailBody = "";

			List<Review> reviews = await AmazonScrapper.ScrapReviewWebsite(url);
			var product = ProductRepo.GetAll().Find(p => p.ASIN == ASIN);
			var storedReviews = ReviewRepo.GetAll();

			foreach (Review dbReview in storedReviews)
			{


				var existingReview = reviews.Find(r => ASIN == dbReview.Product.ASIN
					&& r.ProfileName == dbReview.ProfileName);

				if (existingReview == null)
				{
					mailBody += $"REMOVED: The review of {dbReview.ProfileName} for product with ASIN {dbReview.Product.ASIN} has been removed";
					ReviewRepo.Delete(dbReview);
				}
			}

			foreach (Review review in reviews)
			{
				var existingReview = storedReviews.Find(r => r.ProductId == product.Id
					&& r.ProfileName == review.ProfileName);

				if (existingReview != null)
				{
					if (existingReview.Content != review.Content)
					{
						mailBody += $"UPDATE: The review of {review.ProfileName} for product with ASIN {ASIN} has been updated";
						existingReview.Content = review.Content;
						ReviewRepo.Update(existingReview);
					}
					
				}
				else 
				{
					mailBody += $"NEW: a new review of {review.ProfileName} for product with ASIN {ASIN} has been added";				
					review.Product = product;
					ReviewRepo.Add(review);
				}
			}

			if (!string.IsNullOrEmpty(mailBody))
			{
				EmailSender.Send(mailBody, mailTitle);
			}
		}

		public async Task TrackProducts(IList<string> ASINs)
		{
			var newproducts = new List<Product>();
			var products = ProductRepo.GetAll();
			foreach (var ASIN in ASINs)
			{
				var productExists = await VerifyProductExists(ASIN);
				if (!productExists)
				{
					throw new Exception($"There is not product with this {ASIN} in amazon marketplace");
				}
				var existingProduct = products.Find(p => p.ASIN == ASIN);
				if (existingProduct != null)
				{
					throw new Exception($"This product {ASIN} is already tracked");
				}
				Product newProduct = new Product
				{
					Id = Guid.NewGuid(),
					ASIN = ASIN
				};
				newproducts.Add(newProduct);
			}
			ProductRepo.AddRange(newproducts);
		}

		public List<string> GetTrackedASINs()
		{
			return ProductRepo.GetAll().Select(p => p.ASIN).ToList();
		}

		private async Task<bool> VerifyProductExists(string ASIN)
		{
			IDocument document = await AmazonScrapper.GetPage(amazonProductUrl + ASIN);
			if (document is null)
			{
				return false;
			}
			return true;
		}
	}
}
