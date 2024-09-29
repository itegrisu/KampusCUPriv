using Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.PersonnelManagements
{
    public class PersonnelPassportInfoConfiguration : IEntityTypeConfiguration<PersonnelPassportInfo>
    {
        public void Configure(EntityTypeBuilder<PersonnelPassportInfo> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelPassportInfos).HasForeignKey(y => y.GidPersonnelFK);

            builder.Property(y => y.PassportNo).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.DateOfIssue).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.ValidityDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);



        }
    }
}
