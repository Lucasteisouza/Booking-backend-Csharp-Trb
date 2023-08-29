namespace TrybeHotel.Dto
{
    public class BookingDtoInsert
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestQuant { get; set; }
        public int RoomId { get; set; }
    }

    public class BookingResponse
    {
        public int bookingId { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public int guestQuant { get; set; }
        public RoomDto room { get; set; }
    }
}