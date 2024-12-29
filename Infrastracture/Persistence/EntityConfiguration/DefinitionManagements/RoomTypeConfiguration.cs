using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Code).IsRequired().HasColumnType("varchar").HasMaxLength(10);
            builder.Property(y => y.Capacity).IsRequired().HasColumnType("int");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);

            builder.HasMany(u => u.ReservationDetails).WithOne(y => y.RoomTypeFK).HasForeignKey(y => y.GidRoomTypeFK);
            builder.HasMany(u => u.GuestAccommodationRooms).WithOne(y => y.RoomTypeFK).HasForeignKey(y => y.GidRoomTypeFK);
        }
    }
}
