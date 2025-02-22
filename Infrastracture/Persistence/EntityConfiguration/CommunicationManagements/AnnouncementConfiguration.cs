using Domain.Entities.CommunicationManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.CommunicationManagements
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.ClubFK).WithMany(u => u.Announcements).HasForeignKey(y => y.GidClubFK);

            builder.Property(y => y.Description).IsRequired().HasColumnType("nvarchar").HasMaxLength(1000);
            builder.HasMany(u => u.StudentAnnouncements).WithOne(y => y.AnnouncementFK).HasForeignKey(y => y.GidAnnouncementFK);
        }
    }
}
