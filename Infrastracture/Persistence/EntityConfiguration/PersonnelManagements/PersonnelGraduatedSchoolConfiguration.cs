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

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelGraduatedSchools).HasForeignKey(y => y.GidPersonnelFK);

            builder.Property(y => y.SchoolInfo).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.DepartmentInfo).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.StartYear).IsRequired().HasColumnType("int");
            builder.Property(y => y.GraduationDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}
