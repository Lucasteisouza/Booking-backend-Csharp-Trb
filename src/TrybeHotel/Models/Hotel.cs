namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// 1. Implemente as models da aplicação
public class Hotel 
{
    public int HotelId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int CityId { get; set; }
    public IEnumerable<Room>? Rooms { get; set; }
    public City? City { get; set; }
}