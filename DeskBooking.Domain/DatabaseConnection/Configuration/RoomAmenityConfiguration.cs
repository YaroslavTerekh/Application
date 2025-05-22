using DeskBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DatabaseConnection.Configuration;

public class RoomAmenityConfiguration : IEntityTypeConfiguration<RoomAmenity>
{
    public void Configure(EntityTypeBuilder<RoomAmenity> builder)
    {
        builder.HasKey(ra => new { ra.RoomId, ra.AmenityId });

        builder.HasOne(ra => ra.Room)
               .WithMany(r => r.Amenities)
               .HasForeignKey(ra => ra.RoomId);

        builder.HasOne(ra => ra.Amenity)
               .WithMany(a => a.Rooms)
               .HasForeignKey(ra => ra.AmenityId);
    }
}
