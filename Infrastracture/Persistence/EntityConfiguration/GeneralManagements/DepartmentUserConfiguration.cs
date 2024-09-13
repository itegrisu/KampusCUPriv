using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.GeneralManagements
{
    public class DepartmentUserConfiguration : IEntityTypeConfiguration<DepartmentUser>
    {
        public void Configure(EntityTypeBuilder<DepartmentUser> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.DepartmentFK).WithMany(u => u.DepartmentUsers).HasForeignKey(y => y.GidDepartmanFK);
            builder.HasOne(y => y.UserFK).WithMany(u => u.DepartmentUsers).HasForeignKey(y => y.GidPersonelFK);
        }
    }
}
