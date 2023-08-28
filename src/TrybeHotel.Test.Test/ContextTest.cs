using Microsoft.EntityFrameworkCore;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace trybe_hotel.Test.Test;

public class ContextTest : DbContext, ITrybeHotelContext
{
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Hotel> Hotels { get; set; }
    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }
    public ContextTest(DbContextOptions<ContextTest> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
        .HasMany(c => c.Hotels)
        .WithOne(h => h.City)
        .HasForeignKey(h => h.CityId);


        modelBuilder.Entity<Hotel>()
        .HasMany(r => r.Rooms)
        .WithOne(h => h.Hotel)
        .HasForeignKey(r => r.HotelId);

        modelBuilder.Entity<Room>()
            .HasMany(b => b.Bookings)
            .WithOne(r => r.Room)
            .HasForeignKey(b => b.RoomId);

        modelBuilder.Entity<User>()
            .HasMany(b => b.Bookings)
            .WithOne(u => u.User)
            .HasForeignKey(b => b.UserId);
    }
}