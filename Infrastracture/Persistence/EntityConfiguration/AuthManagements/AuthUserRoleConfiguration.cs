using Domain.Entities.AuthManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.AuthManagements
{
    public class AuthUserRoleConfiguration : IEntityTypeConfiguration<AuthUserRole>
    {
        public void Configure(EntityTypeBuilder<AuthUserRole> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.AuthUserRoles).HasForeignKey(y => y.GidUserFK);
            builder.HasOne(y => y.AuthRoleFK).WithMany(u => u.AuthUserRoles).HasForeignKey(y => y.GidRoleFK);
            builder.HasOne(y => y.AuthPageFK).WithMany(u => u.AuthUserRoles).HasForeignKey(y => y.GidPageFK);

            builder.Property(y => y.RowNo).IsRequired();


        }
    }
}
