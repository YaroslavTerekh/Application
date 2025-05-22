using DeskBooking.Domain.DatabaseConnection.Configuration;
using DeskBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DatabaseConnection;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }

    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<OpenspaceDesk> OpenspaceDesks { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<RoomAmenity> RoomAmenity { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoomAmenityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
