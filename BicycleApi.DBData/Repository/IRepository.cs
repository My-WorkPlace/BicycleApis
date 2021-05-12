using System.Collections.Generic;
using System.Threading.Tasks;

namespace BicycleApi.DBData.Repository
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IEnumerable<TEntity> Get();
		Task<TEntity> GetByIdAsync(int id);
		Task<TEntity> CreateAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TEntity entity);
		Task RemoveAsync(TEntity entity);
	}
}
