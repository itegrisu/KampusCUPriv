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
    public class GuestAccommodationPersonConfiguration : IEntityTypeConfiguration<GuestAccommodationPerson>
    {
        public void Configure(EntityTypeBuilder<GuestAccommodationPerson> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.GuestAccommodationFK).WithMany(u => u.GuestAccommodationPersons).HasForeignKey(y => y.GidGuestAccommodationFK);
            builder.HasOne(y => y.CountryFK).WithMany(u => u.GuestAccommodationPersons).HasForeignKey(y => y.GidNationalityFK);

            builder.Property(y => y.IdNumber).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.FullName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.BirthDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.GuestAccommodationResults).WithOne(y => y.GuestAccommodationPersonFK).HasForeignKey(y => y.GidGuestAccommodationPersonFK);
        }
    }
}
