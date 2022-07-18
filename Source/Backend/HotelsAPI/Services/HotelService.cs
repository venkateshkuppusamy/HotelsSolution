using HotelsAPI.Models;
using HotelsAPI.Repositories;

namespace HotelsAPI.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<PagedList<Hotel>> Get(HotelFilter filter)
        {
            var hotels = (await _hotelRepository.Get()).AsEnumerable();
            
            if (filter.Name != null)
                hotels = hotels.Where(w => w.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
            if (filter.Rating != null)
                hotels = hotels.Where(w => w.Rating.Equals(filter.Rating));

            if (filter.SortField != null && filter.IsSortDesc != null)
            {
                var sortField = filter.SortField;
                var propertyInfo = typeof(Hotel).GetProperty(sortField);
                hotels = filter.IsSortDesc ? hotels.OrderByDescending(o => propertyInfo.GetValue(o, null)) : hotels.OrderBy(o => propertyInfo.GetValue(o, null));
            }
            int totalCount = hotels.Count();
            if (filter.PageSize > 0 && filter.PageIndex > 0)
            {
                hotels = hotels.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize);
            }
            PagedList<Hotel> pagedList = new PagedList<Hotel>(hotels.ToList(), totalCount,  filter.PageIndex, filter.PageSize);
            return pagedList;
        }
    }
}
