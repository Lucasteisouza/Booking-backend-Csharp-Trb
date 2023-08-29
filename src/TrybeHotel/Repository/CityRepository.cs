using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
           return _context.Cities
                .Select(c => new CityDto
                {
                    cityId = c.CityId,
                    name = c.Name,
                    state = c.State
                });
        }

        // 2. Refatore o endpoint POST /city
        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();

            return new CityDto
            {
                cityId = city.CityId,
                name = city.Name,
                state = city.State
            };
        }

        // 3. Desenvolva o endpoint PUT /city
        public CityDto UpdateCity(City city)
        {
            var cityToUpdate = _context.Cities.Find(city.CityId);
            cityToUpdate.Name = city.Name;
            cityToUpdate.State = city.State;
            _context.SaveChanges();

            return new CityDto
            {
                cityId = cityToUpdate.CityId,
                name = cityToUpdate.Name,
                state = cityToUpdate.State
            };
        }

    }
}