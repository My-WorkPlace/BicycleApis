using System.Collections.Generic;
using System.Threading.Tasks;

namespace BicycleApi.DBData.Repository
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAsync();
		Task<TEntity> GetByIdAsync(int id);
		Task<TEntity> CreateAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TEntity entity);
		Task RemoveAsync(TEntity entity);
	}
}
