using Amazon.Scrapper.Entities;
using Amazon.Scrapper.Reviews;
using Amazon.Scrapper.ReviewTracking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amazon.Scrapper.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AmazonScrapperController : ControllerBase
	{

		private readonly ILogger<AmazonScrapperController> _logger;
		private readonly IReviewTracker _reviewTracker;
		private readonly IReviewManager _reviewManager;

		public AmazonScrapperController(
			ILogger<AmazonScrapperController> logger,
			IReviewTracker reviewTracker,
			IReviewManager reviewManager)
		{
			_logger = logger;
			_reviewTracker = reviewTracker;
			_reviewManager = reviewManager;
		}

		[HttpPut]
		[Route("/ScrapReviews")]
		public async Task<ActionResult> ScrapReviews(string url)
		{
			try
			{
				await _reviewTracker.CheckForUpdates(url);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return StatusCode(500);
			}
		}

		[HttpGet]
		[Route("/GetReviews")]
		public List<Review> GetReviews()
		{
			return _reviewManager.GetReviews();
		}

		[HttpGet]
		[Route("/GetReviewsByASIN")]
		public List<Review> GetReviewsByASIN(string ASIN)
		{
			return _reviewManager.GetReviewByASIN(ASIN);
		}

		[HttpPost]
		[Route("/TrackProducts")]
		public async Task<ActionResult> TrackProducts(List<string> url)
		{
			await _reviewTracker.TrackProducts(url);
			return Ok();
		}

		[HttpGet]
		[Route("/GetTrackedProducts")]
		public List<string> GetTrackedProducts()
		{
			return _reviewTracker.GetTrackedASINs();
		}
	}
}
