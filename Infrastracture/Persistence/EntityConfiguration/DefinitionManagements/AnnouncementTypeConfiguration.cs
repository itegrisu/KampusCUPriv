using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class AnnouncementTypeConfiguration : IEntityTypeConfiguration<AnnouncementType>
    {
        public void Configure(EntityTypeBuilder<AnnouncementType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(150);

            builder.HasMany(u => u.Announcements).WithOne(y => y.AnnouncementTypeFK).HasForeignKey(y => y.GidAnnouncementType);
        }
    }
}
