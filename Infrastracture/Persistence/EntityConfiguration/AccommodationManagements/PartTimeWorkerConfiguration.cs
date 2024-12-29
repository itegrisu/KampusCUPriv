using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.AccommodationManagements
{
    public class PartTimeWorkerConfiguration : IEntityTypeConfiguration<PartTimeWorker>
    {
        public void Configure(EntityTypeBuilder<PartTimeWorker> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.IdentityNo).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.FullName).IsRequired().HasColumnType("varchar").HasMaxLength(60);
            builder.Property(y => y.UserName).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Password).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            builder.Property(y => y.PasswordHash).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            builder.Property(y => y.IsLoginStatus).IsRequired().HasColumnType("bit");
            builder.Property(y => y.Gsm).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.BirthDate).IsRequired().HasColumnType("datetime");

            builder.HasMany(u => u.PartTimeWorkerForeignLanguages).WithOne(y => y.PartTimeWorkerFK).HasForeignKey(y => y.GidPartTimeWorkerFK);
            builder.HasMany(u => u.PartTimeWorkerFiles).WithOne(y => y.PartTimeWorkerFK).HasForeignKey(y => y.GidPartTimeWorkerFK);
            builder.HasMany(u => u.ReservationHotelPartTimeWorkers).WithOne(y => y.PartTimeWorkerFK).HasForeignKey(y => y.GidPartTimeWorkerFK);

        }
    }
}
