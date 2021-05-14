using System.Collections.Generic;
using System.Threading.Tasks;
using BicycleApi.DBData.Entities;

namespace BicycleApi.Data.Interfaces
{
	public interface IDetailService
	{
		Task<IEnumerable<Detail>> GetAsync();
		IEnumerable<Detail> Get();
		Task<Detail> GetByIdAsync(int id);
		Task<Detail> UpsertAsync(Detail model);
		Task RemoveAsync(Detail entity);
		Task Commit();
		Task Rollback();
	}
}
