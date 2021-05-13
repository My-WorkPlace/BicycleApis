using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using BicycleApi.Data.Interfaces;
using BicycleApi.Data.Models.Request;
using BicycleApi.DBData.Entities;

using Microsoft.AspNetCore.Mvc;

using Sieve.Models;
using Sieve.Services;

namespace BicycleApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DetailsController : ControllerBase
	{
		private readonly IDetailService _detailService;
		private readonly IMapper _mapper;
		private SieveProcessor _sieveProcessor;

		public DetailsController(IDetailService detailService, IMapper mapper, SieveProcessor sieveProcessor)
		{
			_detailService = detailService;
			_mapper = mapper;
			_sieveProcessor = sieveProcessor;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string filters, string sorts, int? page, int? pageSize)
		{
			var filter = new SieveModel { Filters = filters, Sorts = sorts, Page = page, PageSize = pageSize };
			var response = new List<DetailRequestModel>();
			IQueryable<DetailRequestModel> res = null;
			await Task.Factory.StartNew(() =>
			{
				var source = _detailService.Get().AsQueryable();
				response.AddRange(source.Select(detail => _mapper.Map<DetailRequestModel>(detail)));
				res = _sieveProcessor.Apply(filter, response.AsQueryable()); // Returns `result` after applying the sort/filter/page query in `SieveModel` to it
			});
			return Ok(res.ToList());
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var detail = await _detailService.GetByIdAsync(id);
			return detail == null ? (IActionResult)NotFound() : Ok(_mapper.Map<DetailRequestModel>(detail));
		}

		[HttpPut]
		public async Task<IActionResult> Upsert(DetailRequestModel model)
		{
			var detail = _mapper.Map<Detail>(model);
			var res = await _detailService.UpsertAsync(detail);
			return res == null ? (IActionResult)NotFound() : Ok(_mapper.Map<DetailRequestModel>(res));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var detail = await _detailService.GetByIdAsync(id);
			await _detailService.RemoveAsync(detail);
			return NoContent();
		}
	}
}
