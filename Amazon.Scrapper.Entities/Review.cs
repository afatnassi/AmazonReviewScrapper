using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.Entities
{
	public class Review
	{
		public Guid Id { get; set; }
		public string ProfileName { get; set; }
		public int Rating { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int NumberOfVotes { get; set; }
	}
}
