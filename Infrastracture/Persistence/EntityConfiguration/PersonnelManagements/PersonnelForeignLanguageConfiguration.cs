using Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.PersonnelManagements
{
    public class PersonnelForeignLanguageConfiguration : IEntityTypeConfiguration<PersonnelForeignLanguage>
    {
        public void Configure(EntityTypeBuilder<PersonnelForeignLanguage> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelForeignLanguages).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasOne(y => y.ForeignLanguageFK).WithMany(u => u.PersonnelForeignLanguages).HasForeignKey(y => y.GidLanguageFK);

        }
    }
}
