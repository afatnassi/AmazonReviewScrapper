using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.Entities
{
	public interface IEntity
	{
		Guid Id { get; set; }
	}
}
