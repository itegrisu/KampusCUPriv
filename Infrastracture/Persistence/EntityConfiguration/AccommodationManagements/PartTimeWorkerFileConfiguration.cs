using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.AccommodationManagements
{
    public class PartTimeWorkerFileConfiguration : IEntityTypeConfiguration<PartTimeWorkerFile>
    {
        public void Configure(EntityTypeBuilder<PartTimeWorkerFile> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.PartTimeWorkerFK).WithMany(u => u.PartTimeWorkerFiles).HasForeignKey(y => y.GidPartTimeWorkerFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.WorkerFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.ExpiredDate).IsRequired(false).HasColumnType("datetime");


        }
    }
}
