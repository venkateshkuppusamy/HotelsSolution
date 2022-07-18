using FluentAssertions;
using FluentValidation;
using HotelsAPI.Controllers;
using HotelsAPI.Models;
using HotelsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsAPITests.ControllersTests
{
    public class HotelsControllerTests
    {
        private readonly Mock<IHotelService> _mockHotelService;
        private readonly HotelFilterValidator _hotelValidator;
        
        public HotelsControllerTests()
        {
            _mockHotelService = new Mock<IHotelService>();
            _hotelValidator = new HotelFilterValidator();
        }

        [Fact]
        public async void Get_withNoRating_returnsHotels()
        {
            HotelFilter filter = new HotelFilter();
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }};
            PagedList<Hotel> pagedHotels = new PagedList<Hotel>(hotels, 2, 1, 10);
            _mockHotelService.Setup(s => s.Get(filter)).Returns(Task.FromResult(pagedHotels));
            HotelsController hotelsController = new HotelsController(_mockHotelService.Object, _hotelValidator);
            
            IActionResult result = await hotelsController.Get(filter);
            Assert.IsType<OkObjectResult>(result);
            var hotelsResult = (result as OkObjectResult).Value as PagedList<Hotel>;
            hotelsResult.Items.Should().Equal(hotels);
        }

        [Fact]
        public async void Get_with1Rating_returnsHotels()
        {
            HotelFilter filter = new HotelFilter() { Rating = 1 };
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =1 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =1 }};
            PagedList<Hotel> pagedHotels = new PagedList<Hotel>(hotels, 2, 1, 10);
            _mockHotelService.Setup(s => s.Get(filter)).Returns(Task.FromResult(pagedHotels));
            HotelsController hotelsController = new HotelsController(_mockHotelService.Object, _hotelValidator);

            IActionResult result = await hotelsController.Get(filter);
            Assert.IsType<OkObjectResult>(result);
            var hotelsResult = (result as OkObjectResult).Value as PagedList<Hotel>;
            hotelsResult.Items.Should().Equal(hotels);
        }

        [Fact]
        public async void Get_VerifyHotelServiceGetCall()
        {
            HotelFilter filter = new HotelFilter();
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }};
            PagedList<Hotel> pagedHotels = new PagedList<Hotel>(hotels, 2, 1, 10);
            _mockHotelService.Setup(s => s.Get(filter)).Returns(Task.FromResult(pagedHotels));
            HotelsController hotelsController = new HotelsController(_mockHotelService.Object, _hotelValidator);

            IActionResult result = await hotelsController.Get(filter);

            _mockHotelService.Verify(v => v.Get(filter), Times.Once);
        }

        [Fact]
        public async void Get_WithInvalidRating_returnsErrors()
        {
            HotelFilter filter = new HotelFilter() { Rating = 0 };
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =3 }};
            PagedList<Hotel> pagedHotels = new PagedList<Hotel>(hotels, 2, 1, 10);
            _mockHotelService.Setup(s => s.Get(filter)).Returns(Task.FromResult(pagedHotels));
            HotelsController hotelsController = new HotelsController(_mockHotelService.Object, _hotelValidator);

            IActionResult result = await hotelsController.Get(filter);
            Assert.IsType<BadRequestObjectResult>(result);
            var hotelsResult = (result as BadRequestObjectResult).Value as ErrorResult;
            hotelsResult.Errors.Should().HaveCount(1);
        }
    }
}
