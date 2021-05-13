using System;
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
			Detail fakeDetail = new Detail() { Id = 2, Color = "asd" };
			var mockIRepository = new Mock<IRepository<Detail>>();
			var mockBrandService = new Mock<IBrandService>();
			var mockCountryService = new Mock<ICountryService>();
			mockIRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(fakeDetail);

			var detailService = new DetailService(mockIRepository.Object, mockCountryService.Object, mockBrandService.Object);
			var result = await detailService.GetByIdAsync(1);
			Assert.Equal(1, result.Id);
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
	}
}
