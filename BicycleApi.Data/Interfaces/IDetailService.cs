using System.Collections.Generic;
using System.Threading.Tasks;
using BicycleApi.Data.Models.Request;
using BicycleApi.DBData.Entities;

namespace BicycleApi.Data.Interfaces
{
	public interface IDetailService
	{
		Task<IEnumerable<Detail>> GetAsync();
		Task<Detail> GetByIdAsync(int id);
		Task<Detail> UpsertAsync(DetailRequestModel model);
		Task<Detail> UpdateAsync(Detail entity);
		Task RemoveAsync(Detail entity);
	}
}
