using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.GeneralManagements
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.AsilYoneticFK).WithMany(u => u.AsilYonetilenDepartmants).HasForeignKey(y => y.GidAsilYoneticiFK);
            builder.HasOne(y => y.YedekYoneticiFK).WithMany(u => u.YedekYonetilenDepartmants).HasForeignKey(y => y.GidYedekYoneticiFK).IsRequired(false);

            builder.Property(y => y.DepartmanAdi).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Detay).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.DepartmentUsers).WithOne(y => y.DepartmentFK).HasForeignKey(y => y.GidDepartmanFK);

        }
    }
}