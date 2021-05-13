using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BicycleApi.Data.Interfaces;
using BicycleApi.DBData.Entities;
using BicycleApi.DBData.Repository;

namespace BicycleApi.Data.Services
{
	public class CountryService : ICountryService
	{
		private readonly IRepository<Country> _repository;

		public CountryService(IRepository<Country> repository)
		{
			_repository = repository;
		}

		public async Task<Country> UpsertAsync(string name)
		{
			var country = _repository.Get(n => n.Name == name).SingleOrDefault();
			if (country != null)
			{
				return country;
			}

			var newCountry = new Country() { Name = name };
			return await _repository.CreateAsync(newCountry);
		}

		public async Task<IEnumerable<Country>> GetAsync() => await _repository.GetAsync();

		public async Task<Country> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
		public async Task RemoveAsync(Country entity) => await _repository.RemoveAsync(entity);

		public async Task<Country> UpdateAsync(Country entity)
		{
			var brandForUpdate = await _repository.GetByIdAsync(entity.Id);

			if (brandForUpdate == null) return brandForUpdate;//TODO fix response if null
			brandForUpdate.Name = entity.Name;
			return await _repository.UpdateAsync(brandForUpdate);
		}

	}
}
