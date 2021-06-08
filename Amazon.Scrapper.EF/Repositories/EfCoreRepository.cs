using Amazon.Scrapper.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.EF.Repositories
{
	public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
		where TEntity: class, IEntity
		where TContext: DbContext
	{
		private readonly TContext context;
		public EfCoreRepository(TContext context)
		{
			this.context = context;
		}

		public TEntity Add(TEntity entity)
		{
			context.Set<TEntity>().Add(entity);
			context.SaveChanges();
			return entity;
		}

		public TEntity Delete(TEntity entity)
		{
			context.Set<TEntity>().Remove(entity);
			context.SaveChanges();
			return entity;
		}

		public List<TEntity> GetAll()
		{
			return context.Set<TEntity>().ToList();
		}
	}
}
