using Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.PersonnelManagements
{
    public class PersonnelResidenceInfoConfiguration : IEntityTypeConfiguration<PersonnelResidenceInfo>
    {
        public void Configure(EntityTypeBuilder<PersonnelResidenceInfo> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelResidenceInfos).HasForeignKey(y => y.GidPersonnelFK);

            builder.Property(y => y.SessionSerialNo).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.DateOfIssue).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.ValidityDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
