using AutoMapper;
using BicycleApi.Data.Models.Request;
using BicycleApi.DBData.Entities;

namespace BicycleApi.Map
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//CreateMap<Developer, DeveloperDTO>(); //Map from Developer Object to DeveloperDTO Object

			CreateMap<DetailRequestModel, Detail>() //Map from Developer Object to DeveloperDTO Object
				//.ForMember(detail => detail.Country.Name, source => source.MapFrom(detailRequestModel => detailRequestModel.CountryName)) //Specific Mapping
				.ForPath(detail => detail.Country.Name, source => source.MapFrom(detailRequestModel => detailRequestModel.CountryName)) //Specific Mapping
				//.ForMember(detail => detail.Brand.Name, source => source.MapFrom(detailRequestModel => detailRequestModel.BrandName)); //Specific Mapping
				.ForPath(detail => detail.Brand.Name, source => source.MapFrom(detailRequestModel => detailRequestModel.BrandName)); //Specific Mapping
				
		}
	}
}
