using AutoMapper;
using BicycleApi.Data.Models.Request;
using BicycleApi.DBData.Entities;

namespace BicycleApi.Map
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<DetailRequestModel, Detail>() //Map from DetailRequestModel Object to Detail Object
				.ForPath(detail => detail.Country.Name, source => source
					.MapFrom(detailRequestModel => detailRequestModel.CountryName))
				.ForPath(detail => detail.Brand.Name, source => source
					.MapFrom(detailRequestModel => detailRequestModel.BrandName))
				.ReverseMap();
		}
	}
}
