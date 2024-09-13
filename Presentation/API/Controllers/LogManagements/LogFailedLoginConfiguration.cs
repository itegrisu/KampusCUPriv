using Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.LogManagements
{
    public class LogFailedLoginConfiguration : IEntityTypeConfiguration<LogFailedLogin>
    {
        public void Configure(EntityTypeBuilder<LogFailedLogin> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.Email).IsRequired().HasColumnType("varchar").HasMaxLength(120);
            builder.Property(y => y.Password).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            builder.Property(y => y.IpAddress).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}