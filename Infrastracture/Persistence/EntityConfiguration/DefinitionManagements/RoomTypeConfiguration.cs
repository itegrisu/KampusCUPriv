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

            builder.Property(y => y.OdaTuru).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.OdaKodu).IsRequired().HasColumnType("varchar").HasMaxLength(10);
            builder.Property(y => y.KisiSayisi).IsRequired().HasColumnType("int");
            builder.Property(y => y.Aciklama).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
        }
    }
}
