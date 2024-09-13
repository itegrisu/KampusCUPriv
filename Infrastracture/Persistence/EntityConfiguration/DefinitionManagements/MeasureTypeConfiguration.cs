using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class MeasureTypeConfiguration : IEntityTypeConfiguration<MeasureType>
    {
        public void Configure(EntityTypeBuilder<MeasureType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.OlcuAdi).IsRequired().HasColumnType("varchar").HasMaxLength(100);

        }
    }
}
