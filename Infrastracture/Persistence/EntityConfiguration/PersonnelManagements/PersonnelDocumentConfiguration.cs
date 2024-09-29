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

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelDocuments).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasOne(y => y.DocumentTypeFK).WithMany(u => u.PersonnelDocuments).HasForeignKey(y => y.GidDocumentType);

            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.ValidityDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
