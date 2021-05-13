using System.Collections.Generic;
using System.Threading.Tasks;

using BicycleApi.Data.Interfaces;
using BicycleApi.Data.Models.Request;
using BicycleApi.DBData.Entities;
using BicycleApi.DBData.Repository;

namespace BicycleApi.Data.Services
{

	public class DetailService : IDetailService
	{
		private readonly IRepository<Detail> _repository;
		private readonly IBrandService _brandService;
		private readonly ICountryService _countryService;
		public DetailService(IRepository<Detail> efRepository, ICountryService countryService, IBrandService brandService)
		{
			_repository = efRepository;
			_countryService = countryService;
			_brandService = brandService;
		}

		public async Task<Detail> UpsertAsync(DetailRequestModel model)
		{
			var detail = await _repository.GetByIdAsync(model.Id);
			var brand = await _brandService.UpsertAsync(model.BrandName);
			var country = await _countryService.UpsertAsync(model.CountryName);

			if (detail != null)
			{
				detail.Brand = brand;
				detail.Country = country;
				detail.Type = model.Type;
				detail.Color = model.Color;
				detail.Material = model.Material;
				return await _repository.UpdateAsync(detail);
			}
			var newDetail = new Detail()
			{
				Brand = brand,
				Country = country,
				Type = model.Type,
				Material = model.Material,
				Color = model.Color
			};
			return await _repository.CreateAsync(newDetail);
		}

		public async Task<Detail> UpsertAsync(Detail model)
		{
			var detail = await _repository.GetByIdAsync(model.Id);
			var brand = await _brandService.UpsertAsync(model.Brand.Name);
			var country = await _countryService.UpsertAsync(model.Country.Name);

			if (detail != null)
			{
				detail.Brand = brand;
				detail.Country = country;
				detail.Type = model.Type;
				detail.Color = model.Color;
				detail.Material = model.Material;
				return await _repository.UpdateAsync(detail);
			}
			var newDetail = new Detail()
			{
				Brand = brand,
				Country = country,
				Type = model.Type,
				Material = model.Material,
				Color = model.Color
			};
			return await _repository.CreateAsync(newDetail);
		}

		public async Task<IEnumerable<Detail>> GetAsync() => await _repository.GetAsync();

		public async Task<Detail> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

		public async Task RemoveAsync(Detail entity) => await _repository.RemoveAsync(entity);

	}
}
