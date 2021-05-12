using BicycleApi.DBData.Entities;

namespace BicycleApi.Data.Models.Request
{
	public class DetailRequestModel
	{
		public int Id { get; set; }
		public DetailType Type { get; set; }
		public string Color { get; set; }
		public string Material { get; set; }
		public string BrandName { get; set; }
		public string CountryName { get; set; }
	}
}
