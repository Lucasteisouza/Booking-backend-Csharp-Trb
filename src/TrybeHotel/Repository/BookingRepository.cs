using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var bookingToInsert = new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                UserId = user.UserId,
                RoomId = booking.RoomId
            };
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == booking.RoomId);
            if (booking.GuestQuant > room.Capacity)
            {
                throw new System.Exception("Guest quantity over room capacity");
            }
            _context.Bookings.Add(bookingToInsert);
            _context.SaveChanges();
            var queryResult = from b in _context.Bookings
                              join r in _context.Rooms on b.RoomId equals r.RoomId
                              join h in _context.Hotels on r.HotelId equals h.HotelId
                              join c in _context.Cities on h.CityId equals c.CityId
                              select new BookingResponse
                              {
                                  bookingId = b.BookingId,
                                  checkIn = b.CheckIn,
                                  checkOut = b.CheckOut,
                                  guestQuant = b.GuestQuant,
                                  room = new RoomDto
                                  {
                                        roomId = r.RoomId,
                                        name = r.Name,
                                        capacity = r.Capacity,
                                        image = r.Image,
                                        hotel = new HotelDto
                                        {
                                            hotelId = r.Hotel.HotelId,
                                            name = r.Hotel.Name,
                                            address = r.Hotel.Address,
                                            cityId = h.CityId,
                                            cityName = c.Name,
                                            state = c.State
                                        }
                                  }
                              };
            return queryResult.Last();
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var queryResult = from b in _context.Bookings
                              join r in _context.Rooms on b.RoomId equals r.RoomId
                              join h in _context.Hotels on r.HotelId equals h.HotelId
                              join c in _context.Cities on h.CityId equals c.CityId
                              where b.BookingId == bookingId && b.User.Email == email
                              select new BookingResponse
                              {
                                  bookingId = b.BookingId,
                                  checkIn = b.CheckIn,
                                  checkOut = b.CheckOut,
                                  guestQuant = b.GuestQuant,
                                  room = new RoomDto
                                  {
                                        roomId = r.RoomId,
                                        name = r.Name,
                                        capacity = r.Capacity,
                                        image = r.Image,
                                        hotel = new HotelDto
                                        {
                                            hotelId = r.Hotel.HotelId,
                                            name = r.Hotel.Name,
                                            address = r.Hotel.Address,
                                            cityId = h.CityId,
                                            cityName = c.Name,
                                            state = c.State
                                        }
                                  }
                              };
            if (queryResult.Count() == 0)
            {
                return null;
            }
            return queryResult.Last();
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}