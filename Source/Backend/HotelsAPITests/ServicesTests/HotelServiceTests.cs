using FluentAssertions;
using HotelsAPI.Models;
using HotelsAPI.Repositories;
using HotelsAPI.Services;
using Moq;

namespace HotelsAPITests.ServicesTests
{
    public class HotelServiceTests
    {
        private readonly Mock<IHotelRepository> _mockHotelRepository;

        public HotelServiceTests()
        {
            _mockHotelRepository = new Mock<IHotelRepository>();
        }

        [Fact]
        public async void Get_verifyHotelRepositoryCall()
        {
            HotelService hotelService = new HotelService(_mockHotelRepository.Object);
            HotelFilter filter = new HotelFilter();
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }};
            _mockHotelRepository.Setup(s => s.Get()).Returns(Task.FromResult(hotels));

            var result = await hotelService.Get(filter);

            _mockHotelRepository.Verify(v => v.Get(), Times.Once);
        }

        [Fact]
        public async void Get_returnsHotels()
        {
            HotelService hotelService = new HotelService(_mockHotelRepository.Object);
            HotelFilter filter = new HotelFilter();
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }};
            _mockHotelRepository.Setup(s => s.Get()).Returns(Task.FromResult(hotels));

            var result = await hotelService.Get(filter);
            result.Items.Should().Equal(hotels);
        }

        [Fact]
        public async void Get_searchHotelName_returnsMatchingHotels()
        {
            HotelService hotelService = new HotelService(_mockHotelRepository.Object);
            HotelFilter filter = new HotelFilter() { Name = "hotel" };
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }, new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 }};
            List<Hotel> expectedhotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 }, new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }};

            _mockHotelRepository.Setup(s => s.Get()).Returns(Task.FromResult(hotels));

            var result = await hotelService.Get(filter);
            result.Items.Should().BeEquivalentTo(expectedhotels);
        }

        [Fact]
        public async void Get_searchHotelName_returnsNoHotels()
        {
            HotelService hotelService = new HotelService(_mockHotelRepository.Object);
            HotelFilter filter = new HotelFilter() { Name = "123hotel" };
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }, new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 }};
            
            _mockHotelRepository.Setup(s => s.Get()).Returns(Task.FromResult(hotels));

            var result = await hotelService.Get(filter);
            result.Items.Should().HaveCount(0);
        }

        [Fact]
        public async void Get_filterByRating_returnsHotelsFilterByRating()
        {
            HotelService hotelService = new HotelService(_mockHotelRepository.Object);
            HotelFilter filter = new HotelFilter() { Rating= 3 };
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }, new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 }};

            List<Hotel> expected = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 }};

            _mockHotelRepository.Setup(s => s.Get()).Returns(Task.FromResult(hotels));

            var result = await hotelService.Get(filter);
            result.Items.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void Get_sortByRatingDesc_returnsHotelsWithSort()
        {
            HotelService hotelService = new HotelService(_mockHotelRepository.Object);
            HotelFilter filter = new HotelFilter() { IsSortDesc= true, SortField="Rating" };
            List<Hotel> hotels = new List<Hotel>() { new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }, new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 }};

            List<Hotel> expected = new List<Hotel>() {new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 }, new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
            new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 }};

            _mockHotelRepository.Setup(s => s.Get()).Returns(Task.FromResult(hotels));

            var result = await hotelService.Get(filter);
            Assert.Equal("Hotel_123", result.Items[0].Name);
            Assert.Equal("Hotel_abc", result.Items[1].Name);
            Assert.Equal("Motel_123", result.Items[2].Name);
        }

        [Fact]
        public async void Get_WithPage2AndPageSize2_returnLast2Hotels()
        {
            HotelService hotelService = new HotelService(_mockHotelRepository.Object);
            HotelFilter filter = new HotelFilter() { PageIndex =2, PageSize=2};
            List<Hotel> hotels = new List<Hotel>() { 
                new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
                new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 },
                new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 },
                new Hotel() { Name = "Motel_abc", Description="Motel_abc", Location="Chennai", Rating =3 }};


            List<Hotel> expected = new List<Hotel>()
            {
                new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 },
                new Hotel() { Name = "Motel_abc", Description="Motel_abc", Location="Chennai", Rating =3 }
            };
        

            _mockHotelRepository.Setup(s => s.Get()).Returns(Task.FromResult(hotels));

            var result = await hotelService.Get(filter);
            Assert.Equal(4,result.TotalCount);
            Assert.Equal(2,result.Count);
            Assert.Equal(2,result.TotalPages);

            result.Items.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void Get_WithPage2AndPageSize3_returnNoHotels()
        {
            HotelService hotelService = new HotelService(_mockHotelRepository.Object);
            HotelFilter filter = new HotelFilter() { PageIndex = 3, PageSize = 2 };
            List<Hotel> hotels = new List<Hotel>() {
                new Hotel() { Name = "Hotel_abc", Description="Hotel_abc", Location="Chennai", Rating =3 },
                new Hotel() { Name = "Hotel_123", Description="Hotel_123", Location="Banglore", Rating =4 },
                new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 },
                new Hotel() { Name = "Motel_abc", Description="Motel_abc", Location="Chennai", Rating =3 }};


            List<Hotel> expected = new List<Hotel>()
            {
                new Hotel() { Name = "Motel_123", Description="Motel_123", Location="Banglore", Rating =3 },
                new Hotel() { Name = "Motel_abc", Description="Motel_abc", Location="Chennai", Rating =3 }
            };


            _mockHotelRepository.Setup(s => s.Get()).Returns(Task.FromResult(hotels));

            var result = await hotelService.Get(filter);
            Assert.Equal(4,result.TotalCount);
            Assert.Equal(0,result.Count);
            Assert.Equal(2,result.TotalPages);

            result.Items.Should().BeEmpty();
        }


    }
}
