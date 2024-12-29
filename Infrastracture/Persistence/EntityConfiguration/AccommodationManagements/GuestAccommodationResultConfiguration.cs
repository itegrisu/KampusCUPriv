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
    public class GuestAccommodationResultConfiguration : IEntityTypeConfiguration<GuestAccommodationResult>
    {
        public void Configure(EntityTypeBuilder<GuestAccommodationResult> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.GuestAccommodationPersonFK).WithMany(u => u.GuestAccommodationResults).HasForeignKey(y => y.GidGuestAccommodationPersonFK);
            builder.HasOne(y => y.GuestAccommodationRoomFK).WithMany(u => u.GuestAccommodationResults).HasForeignKey(y => y.GidGuestAccommodationRoomFK);

            builder.Property(y => y.Note).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
        }
    }
}
