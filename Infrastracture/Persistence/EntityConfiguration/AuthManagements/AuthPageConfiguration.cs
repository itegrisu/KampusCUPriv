using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AuthManagements;

namespace Persistence.EntityConfiguration.AuthManagements
{
    public class AuthPageConfiguration : IEntityTypeConfiguration<AuthPage>
    {
        public void Configure(EntityTypeBuilder<AuthPage> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.PageName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.RedirectName).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.PhysicalFilePath).IsRequired().HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.MenuLink).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.PathForAuthCheck).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.IsShowMenu).IsRequired();
            builder.Property(y => y.HelpFileName).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.RowNo).IsRequired();

            builder.HasMany(u => u.AuthRolePages).WithOne(y => y.AuthPageFK).HasForeignKey(y => y.GidPageFK);
            builder.HasMany(u => u.AuthUserRoles).WithOne(y => y.AuthPageFK).HasForeignKey(y => y.GidPageFK);

        }
    }
}