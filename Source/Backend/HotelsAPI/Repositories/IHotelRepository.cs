using HotelsAPI.Models;

namespace HotelsAPI.Repositories
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> Get();
    }
}
