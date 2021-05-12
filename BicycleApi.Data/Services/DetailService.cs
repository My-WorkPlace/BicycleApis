using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

using BicycleApi.DBData.Entities;
using BicycleApi.DBData.Repository;

namespace BicycleApi.Data.Services
{
	public interface IDetailService
	{
		IEnumerable<Detail> Get();
		Task<Detail> GetByIdAsync(int id);
		Task<Detail> CreateAsync(Detail entity);
		Task<Detail> UpdateAsync(Detail entity);
		Task RemoveAsync(Detail entity);
	}

	public class DetailService : IDetailService
	{
		private readonly IRepository<Detail> _repository;
		public DetailService(IRepository<Detail> efRepository)
		{
			_repository = efRepository;
		}

		public async Task<Detail> CreateAsync(Detail entity) => await _repository.CreateAsync(entity);

		public IEnumerable<Detail> Get()
		{
			return  _repository.Get();
		}

		public Task<Detail> GetByIdAsync(int id)
		{
			throw new System.NotImplementedException();
		}

		public Task RemoveAsync(Detail entity)
		{
			throw new System.NotImplementedException();
		}

		public Task<Detail> UpdateAsync(Detail entity)
		{
			throw new System.NotImplementedException();
		}
	}
}
