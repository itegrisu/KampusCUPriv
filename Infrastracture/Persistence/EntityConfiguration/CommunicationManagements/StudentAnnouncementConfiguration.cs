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
    public class StudentAnnouncementConfiguration : IEntityTypeConfiguration<StudentAnnouncement>
    {
        public void Configure(EntityTypeBuilder<StudentAnnouncement> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.StudentAnnouncements).HasForeignKey(y => y.GidUserFK);
            builder.HasOne(y => y.AnnouncementFK).WithMany(u => u.StudentAnnouncements).HasForeignKey(y => y.GidAnnouncementFK);

            builder.Property(y => y.IsRead).IsRequired().HasColumnType("bit");
        }
    }
}
