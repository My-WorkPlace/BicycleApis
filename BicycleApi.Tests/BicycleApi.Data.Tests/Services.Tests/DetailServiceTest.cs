using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BicycleApi.Data.Interfaces;
using BicycleApi.Data.Services;
using BicycleApi.DBData.Entities;
using BicycleApi.DBData.Repository;

using FluentAssertions;

using Moq;

using Xunit;

namespace BicycleApi.Tests.BicycleApi.Data.Tests.Services.Tests
{
	public class DetailServiceTest
	{

		[Fact]
		public void GetShouldReturnAllAsyncTest()
		{
			List<Detail> fakeDetails = new List<Detail>();

			var mockIRepository = new Mock<IRepository<Detail>>();
			var mockBrandService = new Mock<IBrandService>();
			var mockCountryService = new Mock<ICountryService>();

			mockIRepository.Setup(x => x.GetAsync().Result).Returns(() => fakeDetails.AsQueryable());

			fakeDetails.Add(new Detail() { Type = DetailType.Coins, Color = "asd" });

			var detailService = new DetailService(mockIRepository.Object, mockCountryService.Object, mockBrandService.Object);

			var result = detailService.GetAsync().Result.ToList();

			result.Count.Should().Be(1);
		}

		[Fact]
		public void GetShouldReturnAllTest()
		{
			List<Detail> fakeDetails = new List<Detail>();

			var mockIRepository = new Mock<IRepository<Detail>>();
			var mockBrandService = new Mock<IBrandService>();
			var mockCountryService = new Mock<ICountryService>();

			mockIRepository.Setup(x => x.GetWithInclude(x => x.Country, x => x.Brand)).Returns(() => fakeDetails.AsQueryable());

			fakeDetails.Add(new Detail() { Type = DetailType.Coins, Color = "asd" });
			var detailService = new DetailService(mockIRepository.Object, mockCountryService.Object, mockBrandService.Object);
			var result = detailService.Get().ToList();

			result.Count.Should().Be(1);
		}

		[Fact]
		public async Task GetShouldReturnById()
		{
			var fakeDetail = new Detail() { Id = 2, Color = "asd" };
			var fakeDetails = new List<Detail> { fakeDetail };

			var mockIRepository = new Mock<IRepository<Detail>>();
			var mockBrandService = new Mock<IBrandService>();
			var mockCountryService = new Mock<ICountryService>();

			mockIRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(fakeDetail);
			mockIRepository.Setup(x => x.GetWithInclude(detail => detail.Country, detail => detail.Brand)).Returns(fakeDetails);

			var detailService = new DetailService(mockIRepository.Object, mockCountryService.Object, mockBrandService.Object);

			var result = await detailService.GetByIdAsync(2);

			Assert.Equal(2, result.Id);
		}


		[Fact]
		public async Task RemoveAsyncShouldBeExecutedTest()
		{
			var fakeDetail = new Detail();

			var mockIRepository = new Mock<IRepository<Detail>>();
			var mockBrandService = new Mock<IBrandService>();
			var mockCountryService = new Mock<ICountryService>();

			var detailService = new DetailService(mockIRepository.Object, mockCountryService.Object, mockBrandService.Object);
			await detailService.RemoveAsync(fakeDetail);

			mockIRepository.Verify(service => service.RemoveAsync(fakeDetail), Times.Once);
		}

		[Fact]
		public async Task UpsertAsyncShouldCreateTest()
		{
			var fakeCountry = new Country() { Name = "Country" };
			var fakeBrand = new Brand() { Name = "Brand" };
			var fakeDetail = new Detail() { Id = 2, Color = "asd", Country = fakeCountry, Brand = fakeBrand };
			var fakeDetails = new List<Detail> { fakeDetail };

			var mockIRepository = new Mock<IRepository<Detail>>();
			var mockBrandService = new Mock<IBrandService>();
			var mockCountryService = new Mock<ICountryService>();

			mockIRepository.Setup(x => x.GetByIdAsync(fakeDetail.Id).Result).Returns(fakeDetail);
			mockIRepository.Setup(x => x.GetWithInclude(x => x.Country, x => x.Brand)).Returns(fakeDetails);
			mockIRepository.Setup(x => x.UpdateAsync(fakeDetail).Result).Returns(fakeDetail);
			mockIRepository.Setup(x => x.CreateAsync(fakeDetail).Result).Returns(fakeDetail);

			mockCountryService.Setup(x => x.UpsertAsync(fakeCountry.Name).Result).Returns(fakeCountry);

			mockBrandService.Setup(x => x.UpsertAsync(fakeBrand.Name).Result).Returns(fakeBrand);

			var detailService = new DetailService(mockIRepository.Object, mockCountryService.Object, mockBrandService.Object);

			var result = await detailService.UpsertAsync(fakeDetail);

			Assert.Equal(fakeDetail.Id, result.Id);
			Assert.Equal(fakeDetail.Color, result.Color);
			Assert.Equal(fakeDetail.Material, result.Material);
			Assert.Equal(fakeDetail.Type, result.Type);
		}
		//TODO more tests to aim full test coverage
	}
}
