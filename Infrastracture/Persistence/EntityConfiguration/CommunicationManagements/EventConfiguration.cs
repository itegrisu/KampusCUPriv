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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.ClubFK).WithMany(u => u.Events).HasForeignKey(y => y.GidClubFK);

            builder.Property(y => y.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(250);
            builder.Property(y => y.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.EndDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Location).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(300);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(300);
        }
    }
}
