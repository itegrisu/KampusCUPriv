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

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelResidenceInfos).HasForeignKey(y => y.GidPersonelFK);

            builder.Property(y => y.OturumSeriNo).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.VerilisTarihi).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.GecerlilikTarihi).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Belge).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Aciklama).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
