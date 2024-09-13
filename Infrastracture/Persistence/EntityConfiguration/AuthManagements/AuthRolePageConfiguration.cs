using Domain.Entities.AuthManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.AuthManagements
{
    public class AuthRolePageConfiguration : IEntityTypeConfiguration<AuthRolePage>
    {
        public void Configure(EntityTypeBuilder<AuthRolePage> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.AuthRoleFK).WithMany(u => u.AuthRolePages).HasForeignKey(y => y.GidRoleFK);
            builder.HasOne(y => y.AuthPageFK).WithMany(u => u.AuthRolePages).HasForeignKey(y => y.GidPageFK);

            builder.Property(y => y.RowNo).IsRequired();
        }
    }
}