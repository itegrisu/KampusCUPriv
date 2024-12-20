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
    public class AccommodationDateConfiguration : IEntityTypeConfiguration<AccommodationDate>
    {
        public void Configure(EntityTypeBuilder<AccommodationDate> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.ReservationDetailFK).WithMany(u => u.AccommodationDates).HasForeignKey(y => y.GidReservationDetailFK);
            builder.HasOne(y => y.GuestFK).WithMany(u => u.AccommodationDates).HasForeignKey(y => y.GidGuestFK);
            builder.HasOne(y => y.ReservationRoomFK).WithMany(u => u.AccommodationDates).HasForeignKey(y => y.GidRoomNoFK);

            builder.Property(y => y.Date).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.PreviousRoomInfo).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}
