using System.ComponentModel.DataAnnotations;
using BicycleApi.DBData.Entities;
using Sieve.Attributes;

namespace BicycleApi.Data.Models.Request
{
	public class DetailRequestModel
	{
		public int Id { get; set; }

		[Required]
		[Sieve(CanFilter = true, CanSort = true)]
		public DetailType Type { get; set; }

		[Required]
		[Sieve(CanFilter = true, CanSort = true)]
		public string Color { get; set; }

		[Required]
		[Sieve(CanFilter = true, CanSort = true)]
		public string Material { get; set; }

		[Required]
		[Sieve(CanFilter = true, CanSort = true)]
		public string BrandName { get; set; }

		[Required]
		[Sieve(CanFilter = true, CanSort = true)]
		public string CountryName { get; set; }
	}
}
