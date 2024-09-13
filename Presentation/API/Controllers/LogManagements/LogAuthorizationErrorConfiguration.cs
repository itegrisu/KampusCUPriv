using Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.LogManagements
{
    public class LogAuthorizationErrorConfiguration : IEntityTypeConfiguration<LogAuthorizationError>
    {
        public void Configure(EntityTypeBuilder<LogAuthorizationError> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.LogAuthorizationErrors).HasForeignKey(y => y.GidUserFK);

            builder.Property(y => y.IpAddress).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.PageInfo).IsRequired().HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.Operation).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.JSonData).IsRequired(false).HasColumnType("varchar").HasMaxLength(2000);
        }
    }
}