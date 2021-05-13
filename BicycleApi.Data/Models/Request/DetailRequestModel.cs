using BicycleApi.DBData.Entities;
using Sieve.Attributes;

namespace BicycleApi.Data.Models.Request
{
	public class DetailRequestModel
	{
		public int Id { get; set; }

		[Sieve(CanFilter = true, CanSort = true)]
		public DetailType Type { get; set; }

		[Sieve(CanFilter = true, CanSort = true)]
		public string Color { get; set; }

		[Sieve(CanFilter = true, CanSort = true)]
		public string Material { get; set; }

		[Sieve(CanFilter = true, CanSort = true)]
		public string BrandName { get; set; }

		[Sieve(CanFilter = true, CanSort = true)]
		public string CountryName { get; set; }
	}
}
