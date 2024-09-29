using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class PermitTypeConfiguration : IEntityTypeConfiguration<PermitType>
    {
        public void Configure(EntityTypeBuilder<PermitType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);

            builder.HasMany(u => u.PersonnelPermitInfos).WithOne(y => y.PermitTypeFK).HasForeignKey(y => y.GidPermitFK);
        }
    }
}
