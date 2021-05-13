using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using BicycleApi.Data.Interfaces;
using BicycleApi.Data.Models.Request;
using BicycleApi.DBData.Entities;

using Microsoft.AspNetCore.Mvc;

namespace BicycleApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DetailsController : ControllerBase
	{
		private readonly IDetailService _detailService;
		private readonly IMapper _mapper;

		public DetailsController(IDetailService detailService, IMapper mapper)
		{
			_detailService = detailService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			
			var response = new List<DetailRequestModel>();
			await Task.Factory.StartNew(() =>
			{
				var source = _detailService.Get();
				response.AddRange(source.Select(detail => _mapper.Map<DetailRequestModel>(detail)));
			});
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var detail = await _detailService.GetByIdAsyncTest(id);
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
