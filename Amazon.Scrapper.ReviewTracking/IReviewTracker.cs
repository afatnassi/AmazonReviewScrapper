using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amazon.Scrapper.ReviewTracking
{
	public interface IReviewTracker
	{
		Task TrackProducts(IList<string> ASINs);

		Task CheckForUpdates(string url);

		List<string> GetTrackedASINs();
	}
}
