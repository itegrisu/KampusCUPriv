using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.AccommodationManagements
{
    public class ReservationHotelStaffConfiguration : IEntityTypeConfiguration<ReservationHotelStaff>
    {
        public void Configure(EntityTypeBuilder<ReservationHotelStaff> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.ReservationHotelStaffs).HasForeignKey(y => y.GidHotelFK);

            builder.Property(y => y.FullName).IsRequired().HasColumnType("varchar").HasMaxLength(60);
            builder.Property(y => y.GsmNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Password).IsRequired(false).HasColumnType("varchar").HasMaxLength(255);
            builder.Property(y => y.PasswordHash).IsRequired(false).HasColumnType("varchar").HasMaxLength(255);
        }
    }
}
