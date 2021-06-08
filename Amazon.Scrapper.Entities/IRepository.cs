using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.Entities
{
	public interface IRepository<T> where T : class, IEntity
	{
		List<T> GetAll();
		T Add(T entity);
		T Delete(T entity);
	}
}
