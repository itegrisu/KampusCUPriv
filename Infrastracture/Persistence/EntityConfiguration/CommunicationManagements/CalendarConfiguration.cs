using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CommunicationManagements;

namespace Persistence.EntityConfiguration.CommunicationManagements
{
    public class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.EventFK).WithMany(u => u.Calendars).HasForeignKey(y => y.GidEventFK);

            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Date).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Color).IsRequired(false).HasColumnType("varchar").HasMaxLength(7);
        }
    }
}
