using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.AccommodationManagements
{
    public class ReservationHotelConfiguration : IEntityTypeConfiguration<ReservationHotel>
    {
        public void Configure(EntityTypeBuilder<ReservationHotel> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.ReservationFK).WithMany(u => u.ReservationHotels).HasForeignKey(y => y.GidReservationFK);
            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.ReservationHotels).HasForeignKey(y => y.GidHotelFK);
            builder.HasOne(y => y.BuyCurrencyFK).WithMany(u => u.BuyReservationHotels).HasForeignKey(y => y.GidBuyCurrencyTypeFK).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(y => y.SellCurrencyFK).WithMany(u => u.SellReservationHotels).HasForeignKey(y => y.GidSellCurrencyTypeFK).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ReservationDetails).WithOne(y => y.ReservationHotelFK).HasForeignKey(y => y.GidReservationHotelFK);
            builder.HasMany(u => u.ReservationHotelPartTimeWorkers).WithOne(y => y.ReservationHotelFK).HasForeignKey(y => y.GidHotelFK);

        }
    }
}
