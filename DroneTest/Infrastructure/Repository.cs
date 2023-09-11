using DroneCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DroneTest.Infrastructure
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected ApplicationDbContext _context;
		protected DbSet<TEntity> dbSet;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
			dbSet = context.Set<TEntity>();
		}

		public virtual IEnumerable<TEntity> Find()
		{
			return dbSet.ToList();
		}

		public virtual TEntity FindOne(string id)
		{
			return dbSet.Find(id);
		}

		public virtual void Add(TEntity entity)
		{
			dbSet.Add(entity);
		}

		public virtual void Update(TEntity entity)
		{
			dbSet.Update(entity);
		}

		public virtual void Remove(TEntity entity)
		{
			dbSet.Remove(entity);
		}
	}
}
