using System.Collections.Generic;
using System.Threading.Tasks;
using BicycleApi.DBData.Entities;

namespace BicycleApi.Data.Interfaces
{
	public interface ICountryService
	{
		Task<IEnumerable<Country>> GetAsync();
		Task<Country> GetByIdAsync(int id);
		Task<Country> UpsertAsync(string name);
		Task<Country> UpdateAsync(Country entity);
		Task RemoveAsync(Country entity);
	}
}
