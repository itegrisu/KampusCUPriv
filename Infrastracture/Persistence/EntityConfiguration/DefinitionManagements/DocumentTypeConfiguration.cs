using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.BelgeAdi).IsRequired().HasColumnType("varchar").HasMaxLength(100);

            builder.HasMany(u => u.PersonnelDocuments).WithOne(y => y.DocumentTypeFK).HasForeignKey(y => y.GidBelgeTuru);
        }
    }
}
