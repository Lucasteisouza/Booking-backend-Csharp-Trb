using System.Net.Http;
using TrybeHotel.Dto;
using TrybeHotel.Repository;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
         private readonly HttpClient _client;
        public GeoService(HttpClient client)
        {
            _client = client;
        }

        // 11. Desenvolva o endpoint GET /geo/status
        public async Task<object> GetGeoStatus()
        {
            var response = await _client.GetAsync("https://nominatim.openstreetmap.org/status.php?format=json");
            if (!response.IsSuccessStatusCode)
            {
                return default(Object);
            }
            var content = await response.Content.ReadFromJsonAsync<object>();
            return content;
        }
        
        // 12. Desenvolva o endpoint GET /geo/address
        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?street={geoDto.Address}&city={geoDto.City}&country=Brazil&state={geoDto.State}&format=json&limit=1");
            requestMessage.Headers.Add("User-Agent", "aspnet-user-agent");
            requestMessage.Headers.Add("Accept", "application/json");
            var response = await _client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                return default(GeoDtoResponse);
            }
            var content = await response.Content.ReadFromJsonAsync<GeoDtoResponse[]>();
            return content.Last();
        }

        // 12. Desenvolva o endpoint GET /geo/address
        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var targetGeoLocation = await GetGeoLocation(geoDto);
            if (targetGeoLocation == null)
            {
                return default(List<GeoDtoHotelResponse>);
            }
            var hotels = repository.GetHotels();
            var hotelsByGeo = new List<GeoDtoHotelResponse>();
            foreach (var hotel in hotels)
            {
                var requestMessage = new GeoDto
                {
                    Address = hotel.address,
                    City = hotel.cityName,
                    State = hotel.state
                };
                var hotelGeoLocation = await GetGeoLocation(requestMessage);
                if (hotelGeoLocation == null)
                {
                    return default(List<GeoDtoHotelResponse>);
                }
                var distance = CalculateDistance(targetGeoLocation.lat, targetGeoLocation.lon, hotelGeoLocation.lat, hotelGeoLocation.lon);
                var hotelByGeo = new GeoDtoHotelResponse
                {
                    HotelId = hotel.hotelId,
                    Name = hotel.name,
                    Address = hotel.address,
                    CityName = hotel.cityName,
                    State = hotel.state,
                    Distance = distance
                };
                hotelsByGeo.Add(hotelByGeo);
            }
            return hotelsByGeo.OrderBy(h => h.Distance).ToList();
        }

        public int CalculateDistance (string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny) {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.',','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.',','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.',','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.',','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double distance = R * c;
            return int.Parse(Math.Round(distance,0).ToString());
        }

        public double radiano(double degree) {
            return degree * Math.PI / 180;
        }

    }
}