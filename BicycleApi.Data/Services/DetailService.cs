using BicycleApi.DBData.Entities;
using BicycleApi.DBData.Repository;

namespace BicycleApi.Data.Services
{
	 public class DetailService
	{
		private readonly IRepository<Detail> _repository;
		public DetailService(IRepository<Detail> efRepository)
		{
			_repository = efRepository;
		}
	}
}
