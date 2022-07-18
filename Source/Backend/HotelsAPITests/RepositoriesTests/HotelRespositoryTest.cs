using HotelsAPI.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsAPITests.RepositoriesTests
{
    public class HotelRespositoryTest
    {
        [Fact]
        public async void Get_returnHotels()
        {
            var mockFileSystem = new Mock<IFileSystem>();
            HotelRepository hotelRepository = new HotelRepository(mockFileSystem.Object);
            string fileText = @"[{""Name"":""ROUGHIES"",""Description"":""Laboriscommodominimquisintquisduis."",""Location"":""londonwest"",""Rating"":3},{""Name"":""MAINELAND"",""Description"":""Idveniaminidesseexdolore."",""Location"":""londoncentral"",""Rating"":4}]";
            mockFileSystem.Setup(s => s.File.ReadAllTextAsync(It.IsAny<string>(),It.IsAny<CancellationToken>())).Returns(Task.FromResult(fileText));
            var result = await hotelRepository.Get();

            Assert.Equal(2, result.Count);

        }
    }
}
