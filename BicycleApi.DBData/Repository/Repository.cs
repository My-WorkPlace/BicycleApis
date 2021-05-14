using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BicycleApi.DBData.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly AppDBContext _dbContext;
		private readonly DbSet<TEntity> _dbSet;

		public Repository(AppDBContext context)
		{
			_dbContext = context;
			_dbSet = context.Set<TEntity>();
		}

		public async Task<TEntity> CreateAsync(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbSet.AddAsync(entity);
			return entity;
		}

		public async Task RemoveAsync(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public  async Task<IEnumerable<TEntity>> GetAsync() => await _dbSet.AsNoTracking().ToListAsync();
		public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
		{
			return _dbSet.AsNoTracking().Where(predicate);
		}

		public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			if(entity == null) throw new ArgumentNullException("entity");
			_dbContext.Entry(entity).State = EntityState.Modified;
			return entity;
		}
		public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			return Include(includeProperties).ToList();
		}

		public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
			params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = Include(includeProperties);
			return query.Where(predicate).ToList();
		}

		private IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = _dbSet.AsNoTracking();
			return includeProperties
				.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
		}

		public TEntity Item(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			foreach (var property in includeProperties)
			{
				_dbSet.Include(property);
			}
			return _dbSet.Where(wherePredicate).FirstOrDefault();
		}

		public async Task Commit()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task Rollback()
		{
			await _dbContext.DisposeAsync();
		}
	}
}
