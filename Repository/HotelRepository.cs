using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Desenvolva o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            var queryResult = from h in _context.Hotels
                              join c in _context.Cities on h.CityId equals c.CityId
                              select new HotelDto
                              {
                                  hotelId = h.HotelId,
                                  name = h.Name,
                                  address = h.Address,
                                  cityId = h.CityId,
                                  cityName = c.Name,
                                  state = c.State,
                              };
            return queryResult;
        }
        
        // 5. Desenvolva o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
            var queryResult = from h in _context.Hotels
                              join c in _context.Cities on h.CityId equals c.CityId
                              select new HotelDto
                              {
                                  hotelId = h.HotelId,
                                  name = h.Name,
                                  address = h.Address,
                                  cityId = h.CityId,
                                  cityName = c.Name,
                                  state = c.State,
                              };
            return queryResult.Last();
        }
    }
}