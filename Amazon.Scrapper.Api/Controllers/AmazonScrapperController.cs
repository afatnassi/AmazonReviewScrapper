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

		public AmazonScrapperController(ILogger<AmazonScrapperController> logger)
		{
			_logger = logger;
		}
	}
}
