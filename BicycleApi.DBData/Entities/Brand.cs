using System.ComponentModel.DataAnnotations;

namespace BicycleApi.DBData.Entities
{
	public class Brand
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
