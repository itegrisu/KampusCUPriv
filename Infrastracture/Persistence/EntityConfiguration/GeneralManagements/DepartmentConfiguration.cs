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

            builder.HasOne(y => y.MainAdminFK).WithMany(u => u.AsilYonetilenDepartmants).HasForeignKey(y => y.GidMainAdminFK);
            builder.HasOne(y => y.CoAdminFK).WithMany(u => u.YedekYonetilenDepartmants).HasForeignKey(y => y.GidCoAdminFK).IsRequired(false);

            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Details).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.DepartmentUsers).WithOne(y => y.DepartmentFK).HasForeignKey(y => y.GidDepartmentFK);

        }
    }
}