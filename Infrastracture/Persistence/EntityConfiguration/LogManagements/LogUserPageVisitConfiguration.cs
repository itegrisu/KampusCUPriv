using Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.LogManagements
{
    public class LogUserPageVisitConfiguration : IEntityTypeConfiguration<LogUserPageVisit>
    {
        public void Configure(EntityTypeBuilder<LogUserPageVisit> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.LogUserPageActions).HasForeignKey(y => y.GidUserFK);

            builder.Property(y => y.IpAddress).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.PageInfo).IsRequired().HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.SessionId).IsRequired().HasColumnType("varchar").HasMaxLength(100);


        }
    }
}
