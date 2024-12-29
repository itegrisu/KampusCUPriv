using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.AccommodationManagements
{
    public class ReservationHotelPartTimeWorkerConfiguration : IEntityTypeConfiguration<ReservationHotelPartTimeWorker>
    {
        public void Configure(EntityTypeBuilder<ReservationHotelPartTimeWorker> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.ReservationHotelFK).WithMany(u => u.ReservationHotelPartTimeWorkers).HasForeignKey(y => y.GidHotelFK);
            builder.HasOne(y => y.PartTimeWorkerFK).WithMany(u => u.ReservationHotelPartTimeWorkers).HasForeignKey(y => y.GidPartTimeWorkerFK);

            builder.Property(y => y.IsActive).IsRequired().HasColumnType("bit");
            builder.Property(y => y.Note).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);


        }
    }
}
