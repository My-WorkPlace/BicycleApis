using System.Collections.Generic;
using System.Threading.Tasks;
using BicycleApi.DBData.Entities;

namespace BicycleApi.Data.Interfaces
{
	public interface IBrandService
	{
		Task<IEnumerable<Brand>> GetAsync();
		Task<Brand> GetByIdAsync(int id);
		Task<Brand> UpsertAsync(string name);
		Task<Brand> UpdateAsync(Brand entity);
		Task RemoveAsync(Brand entity);
	}
}
