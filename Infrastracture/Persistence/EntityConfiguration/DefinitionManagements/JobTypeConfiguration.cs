using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class JobTypeConfiguration : IEntityTypeConfiguration<JobType>
    {
        public void Configure(EntityTypeBuilder<JobType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);

        }
    }
}
