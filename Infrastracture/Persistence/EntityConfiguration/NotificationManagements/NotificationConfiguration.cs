using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.NotificationManagements;

namespace Persistence.EntityConfiguration.NotificationManagements
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.Notifications).HasForeignKey(y => y.GidUserFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.ReadingDate).IsRequired(false);
            builder.Property(y => y.ReadingIp).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Content).IsRequired().HasColumnType("varchar").HasMaxLength(250);
        }
    }
}