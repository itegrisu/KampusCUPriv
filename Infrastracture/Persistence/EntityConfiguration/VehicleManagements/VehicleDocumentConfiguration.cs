using Domain.Entities.VehicleManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.VehicleManagements
{
    public class VehicleDocumentConfiguration : IEntityTypeConfiguration<VehicleDocument>
    {
        public void Configure(EntityTypeBuilder<VehicleDocument> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.VehicleAllFK).WithMany(u => u.VehicleDocuments).HasForeignKey(y => y.GidVehicleFK);
            builder.HasOne(y => y.DocumentTypeFK).WithMany(u => u.VehicleDocuments).HasForeignKey(y => y.GidDocumentType);

            builder.Property(y => y.DocumentName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.DocumentDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.DocumentLastDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.DocumentFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}
