using Domain.Entities.TransportationManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.TransportationManagements
{
    public class TransportationPersonnelConfiguration : IEntityTypeConfiguration<TransportationPersonnel>
    {
        public void Configure(EntityTypeBuilder<TransportationPersonnel> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.TransportationServiceFK).WithMany(u => u.TransportationPersonnels).HasForeignKey(y => y.GidTransportationServiceFK);
            builder.HasOne(y => y.UserFK).WithMany(u => u.TransportationPersonnels).HasForeignKey(y => y.GidStaffPersonnelFK);

            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}
