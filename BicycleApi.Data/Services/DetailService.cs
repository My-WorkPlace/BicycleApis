using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BicycleApi.Data.Helpers;
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

		//public async Task<Detail> UpsertAsync(DetailRequestModel model)
		//{
		//	var detail = await _repository.GetByIdAsync(model.Id);
		//	var brand = await _brandService.UpsertAsync(model.BrandName);
		//	var country = await _countryService.UpsertAsync(model.CountryName);

		//	if (detail != null)
		//	{
		//		detail.Brand = brand;
		//		detail.Country = country;
		//		detail.Type = model.Type;
		//		detail.Color = model.Color;
		//		detail.Material = model.Material;
		//		return await _repository.UpdateAsync(detail);
		//	}
		//	var newDetail = new Detail()
		//	{
		//		Brand = brand,
		//		Country = country,
		//		Type = model.Type,
		//		Material = model.Material,
		//		Color = model.Color
		//	};
		//	return await _repository.CreateAsync(newDetail);
		//}

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
			var newDetail = new Detail(model.Type, model.Color, model.Material) { Brand = brand, Country = country };
			return await _repository.CreateAsync(newDetail);
		}

		public async Task<IEnumerable<Detail>> GetAsync() => await _repository.GetAsync();
		public IEnumerable<Detail> Get() => _repository.GetWithInclude(x => x.Country, x => x.Brand);

		//public async Task<Detail> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
		public async Task<Detail> GetByIdAsync(int id)
		{
			var response = new Detail();
			await Task.Factory.StartNew(() =>
			{
				response = _repository.GetWithInclude(x => x.Country, x => x.Brand).FirstOrDefault(d => d.Id == id);

			});

			return response;
		}

		public async Task RemoveAsync(Detail entity) => await _repository.RemoveAsync(entity);

		//public List<Detail> GetDetails(SortingParams sortingParams)
		//{
		//	var details = _repository.GetWithInclude(x => x.Country, x => x.Brand).AsQueryable();
		//	//var query = this._repository. AsQueryable();

		//	if (!string.IsNullOrEmpty(sortingParams.SortBy))
		//		details = details.Sort(sortingParams.SortBy);

		//	return details.ToList();
		//}

		private void ApplySort(ref IEnumerable<Detail> owners, string orderByQueryString)
		{
			if (!owners.Any())
				return;

			if (string.IsNullOrWhiteSpace(orderByQueryString))
			{
				owners = owners.OrderBy(x => x.Type);
				return;
			}

			var orderParams = orderByQueryString.Trim().Split(',');
			var propertyInfos = typeof(Detail).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var orderQueryBuilder = new StringBuilder();

			foreach (var param in orderParams)
			{
				if (string.IsNullOrWhiteSpace(param))
					continue;

				var propertyFromQueryName = param.Split(' ')[0];
				var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

				if (objectProperty == null)
					continue;

				var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

				orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
			}

			var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

			if (string.IsNullOrWhiteSpace(orderQuery))
			{
				owners = owners.OrderBy(x => x.Type);
				return;
			}

			owners = owners.OrderBy(x => x.Type);
		}

	}
}
