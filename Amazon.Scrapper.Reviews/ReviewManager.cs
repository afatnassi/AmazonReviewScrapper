using Amazon.Scrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.Reviews
{
	public class ReviewManager : IReviewManager
	{
		private IRepository<Review> _reviewRepo { get; set; }
		private IRepository<Product> _productRepo { get; set; }
		public ReviewManager(
			IRepository<Review> reviewRepo,
			IRepository<Product> productRepo)
		{
			_reviewRepo = reviewRepo;
			_productRepo = productRepo;
		}
		public List<Review> GetReviews()
		{
			return _reviewRepo.GetAll();
		}

		public List<Review> GetReviewByASIN(string ASIN)
		{
			var product = _productRepo.GetAll().FirstOrDefault(p => p.ASIN == ASIN);
			if (product == null)
			{
				throw new Exception($"No Product with this ASIN {ASIN} is being tracked");
			}
			return _reviewRepo.GetAll().Where(r => r.ProductId == product.Id).ToList();
		}
	}
}
