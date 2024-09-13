using Domain.Entities.AuthManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.AuthManagements
{
    public class AuthRoleConfiguration : IEntityTypeConfiguration<AuthRole>
    {
        public void Configure(EntityTypeBuilder<AuthRole> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.RoleName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.RoleDescription).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.IconImage).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.RowNo).IsRequired();
            builder.HasMany(u => u.AuthRolePages).WithOne(y => y.AuthRoleFK).HasForeignKey(y => y.GidRoleFK);
            builder.HasMany(u => u.AuthUserRoles).WithOne(y => y.AuthRoleFK).HasForeignKey(y => y.GidRoleFK);
        }
    }
}
