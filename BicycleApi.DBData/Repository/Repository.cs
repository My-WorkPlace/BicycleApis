using System.Collections.Generic;
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
			await _dbSet.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public async Task RemoveAsync(TEntity entity)
		{
			_dbSet.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public IEnumerable<TEntity> Get() => _dbSet;

		public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
			return entity;
		}
	}
}
