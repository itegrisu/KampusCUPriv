using Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.PersonnelManagements
{
    public class PersonnelPermitInfoConfiguration : IEntityTypeConfiguration<PersonnelPermitInfo>
    {
        public void Configure(EntityTypeBuilder<PersonnelPermitInfo> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelPermitInfos).HasForeignKey(y => y.GidPersonelFK);
            builder.HasOne(y => y.PermitTypeFK).WithMany(u => u.PersonnelPermitInfos).HasForeignKey(y => y.GidPermitFK);

            builder.Property(y => y.IzinBaslamaTarihi).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.IzinBitisTarihi).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Belge).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Aciklama).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}
