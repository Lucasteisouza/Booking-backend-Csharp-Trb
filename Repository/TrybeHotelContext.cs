using Microsoft.EntityFrameworkCore;
using TrybeHotel.Models;

namespace TrybeHotel.Repository;
public class TrybeHotelContext : DbContext, ITrybeHotelContext
{
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Hotel> Hotels { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public TrybeHotelContext(DbContextOptions<TrybeHotelContext> options) : base(options) {
        
    }
    public TrybeHotelContext() { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var connectionString = "Server=containers-us-west-65.railway.app;User Id=root;Password=V2FVED8UPN1OgVYDbivL;Port=6478;Database=railway;";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), null);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {}

}