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
			var details = await _detailService.GetAsync();
			return Ok(details);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var detail = await _detailService.GetByIdAsync(id);
			return detail == null ? (IActionResult)NotFound() : Ok(detail);
		}

		//[HttpPost]
		//public async Task<IActionResult> Create(DetailRequestModel model)
		//{
		//	var detail = await _detailService.UpsertAsync(model);
		//	return Ok(detail);
		//}

		[HttpPut]
		public async Task<IActionResult> Upsert(DetailRequestModel model)
		{
			var developerDtoMapped = _mapper.Map<Detail>(model);
			var res = await _detailService.UpsertAsync(developerDtoMapped);
			return res == null ? (IActionResult)NotFound() : Ok(res);

			//var res = await _detailService.UpsertAsync(model);
			//return res == null ? (IActionResult)NotFound() : Ok(res);
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
