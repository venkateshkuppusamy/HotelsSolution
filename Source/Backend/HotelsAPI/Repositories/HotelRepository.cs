using HotelsAPI.Models;
using Newtonsoft.Json;
using System.IO.Abstractions;

namespace HotelsAPI.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly IFileSystem _fileSystem;

        public HotelRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
       
        public async Task<List<Hotel>> Get()
        {
            string path = Directory.GetCurrentDirectory() + @"\Data\Hotels.Json";
            
            string text = await _fileSystem.File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<List<Hotel>>(text);

        }
    }
}
