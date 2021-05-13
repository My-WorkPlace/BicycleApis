using BicycleApi.Data.Interfaces;
using BicycleApi.DBData.Entities;
using BicycleApi.DBData.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApi.Data.Services
{
	public class BrandService : IBrandService
	{
		private readonly IRepository<Brand> _repository;

		public BrandService(IRepository<Brand> repository)
		{
			_repository = repository;
		}

		public async Task<Brand> UpsertAsync(string name)
		{
			var brand = _repository.Get(n => n.Name == name).SingleOrDefault();
			if (brand != null)
			{
				return brand;
			}

			var newBrand = new Brand() {Name = name};
			return await _repository.CreateAsync(newBrand);
		}

		public async Task<IEnumerable<Brand>> GetAsync() => await _repository.GetAsync();

		public async Task<Brand> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
		public async Task RemoveAsync(Brand entity) => await _repository.RemoveAsync(entity);

		public async Task<Brand> UpdateAsync(Brand entity)
		{
			var brandForUpdate = await _repository.GetByIdAsync(entity.Id);

			if (brandForUpdate == null) return brandForUpdate;//TODO fix response if null
			brandForUpdate.Name = entity.Name;
			return await _repository.UpdateAsync(brandForUpdate);
		}
	}
}
