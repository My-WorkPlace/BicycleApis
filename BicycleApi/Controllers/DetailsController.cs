using System.Threading.Tasks;

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

		public DetailsController(IDetailService detailService)
		{
			_detailService = detailService;
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

		[HttpPost]
		public async Task<IActionResult> Create(DetailRequestModel model)
		{
			var detail = await _detailService.UpsertAsync(model);
			return Ok(detail);
		}

		[HttpPut]
		public async Task<IActionResult> Update(Detail category)
		{
			var res = await _detailService.UpdateAsync(category);
			return res == null ? (IActionResult)NotFound() : Ok(res);
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
