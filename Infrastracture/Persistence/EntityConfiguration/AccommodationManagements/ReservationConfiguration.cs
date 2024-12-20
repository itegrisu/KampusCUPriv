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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OrganizationFK).WithMany(u => u.Reservations).HasForeignKey(y => y.GidOrganizationFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.EndDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.EstimatedGuestCount).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.EstimatedAccommodationCount).IsRequired(false).HasColumnType("int");

            builder.HasMany(u => u.ReservationHotels).WithOne(y => y.ReservationFK).HasForeignKey(y => y.GidReservationFK);
        }
    }
}
