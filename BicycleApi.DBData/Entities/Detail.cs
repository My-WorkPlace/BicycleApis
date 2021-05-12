namespace BicycleApi.DBData.Entities
{
	public class Detail
	{
		public int Id { get; set; }
		public DetailType Type { get; set; }
		public string Color { get; set; }
		public string Material { get; set; }

		//Navigate fields
		public int BrandId { get; set; }
		public virtual Brand Brand { get; set; }
		public int CountryId { get; set; }
		public virtual Country Country { get; set; }
	}
}
