using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class ForeignLanguageConfiguration : IEntityTypeConfiguration<ForeignLanguage>
    {
        public void Configure(EntityTypeBuilder<ForeignLanguage> builder)
        {

            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.LanguageCode).IsRequired(false).HasColumnType("varchar").HasMaxLength(5);

            builder.HasMany(u => u.PersonnelForeignLanguages).WithOne(y => y.ForeignLanguageFK).HasForeignKey(y => y.GidLanguageFK);

        }
    }
}
