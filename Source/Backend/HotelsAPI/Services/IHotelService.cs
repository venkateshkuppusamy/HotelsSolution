using HotelsAPI.Models;

namespace HotelsAPI.Services
{
    public interface IHotelService
    {
        Task<PagedList<Hotel>> Get(HotelFilter filter);
    }
}
