using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.AccommodationManagements
{
    public class GuestAccommodationRoomConfiguration : IEntityTypeConfiguration<GuestAccommodationRoom>
    {
        public void Configure(EntityTypeBuilder<GuestAccommodationRoom> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.GuestAccommodationFK).WithMany(u => u.GuestAccommodationRooms).HasForeignKey(y => y.GidGuestAccommodationFK);
            builder.HasOne(y => y.RoomTypeFK).WithMany(u => u.GuestAccommodationRooms).HasForeignKey(y => y.GidRoomTypeFK);

            builder.Property(y => y.Date).IsRequired().HasColumnType("datetime");

            builder.HasMany(u => u.GuestAccommodationResults).WithOne(y => y.GuestAccommodationRoomFK).HasForeignKey(y => y.GidGuestAccommodationRoomFK);

        }
    }
}
