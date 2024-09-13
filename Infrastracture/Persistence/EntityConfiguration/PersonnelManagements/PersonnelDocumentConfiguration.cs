using Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.PersonnelManagements
{
    public class PersonnelDocumentConfiguration : IEntityTypeConfiguration<PersonnelDocument>
    {
        public void Configure(EntityTypeBuilder<PersonnelDocument> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelDocuments).HasForeignKey(y => y.GidPersonelFK);
            builder.HasOne(y => y.DocumentTypeFK).WithMany(u => u.PersonnelDocuments).HasForeignKey(y => y.GidBelgeTuru);

            builder.Property(y => y.BelgeAdi).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.GecerlilikTarihi).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.Belge).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Aciklama).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
