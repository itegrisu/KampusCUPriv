using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.AccommodationManagements
{
    public class PartTimeWorkerForeignLanguageConfiguration : IEntityTypeConfiguration<PartTimeWorkerForeignLanguage>
    {
        public void Configure(EntityTypeBuilder<PartTimeWorkerForeignLanguage> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.PartTimeWorkerFK).WithMany(u => u.PartTimeWorkerForeignLanguages).HasForeignKey(y => y.GidPartTimeWorkerFK);
            builder.HasOne(y => y.ForeignLanguageFK).WithMany(u => u.PartTimeWorkerForeignLanguages).HasForeignKey(y => y.GidForeignLanguageFK);



        }
    }
}
