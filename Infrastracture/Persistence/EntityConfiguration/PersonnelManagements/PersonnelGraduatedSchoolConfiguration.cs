using Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.PersonnelManagements
{
    public class PersonnelGraduatedSchoolConfiguration : IEntityTypeConfiguration<PersonnelGraduatedSchool>
    {
        public void Configure(EntityTypeBuilder<PersonnelGraduatedSchool> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelGraduatedSchools).HasForeignKey(y => y.GidPersonelFK);

            builder.Property(y => y.OkulBilgisi).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.BolumBilgisi).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.BaslamaYili).IsRequired().HasColumnType("int");
            builder.Property(y => y.MezuniyetTarihi).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.Belge).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Aciklama).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}
