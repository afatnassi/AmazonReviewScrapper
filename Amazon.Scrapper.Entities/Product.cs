using System;
using System.Collections.Generic;

namespace Amazon.Scrapper.Entities
{
	public class Product
	{
		public Product()
		{
			Reviews = new HashSet<Review>();
		}

		public Guid Id { get; set; }
		public string ASIN { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public virtual ICollection<Review> Reviews { get; set; }
	}
}
