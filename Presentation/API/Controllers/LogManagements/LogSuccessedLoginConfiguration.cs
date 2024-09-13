using Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.LogManagements
{
    public class LogSuccessedLoginConfiguration : IEntityTypeConfiguration<LogSuccessedLogin>
    {
        public void Configure(EntityTypeBuilder<LogSuccessedLogin> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.LogSuccessedLogins).HasForeignKey(y => y.GidUserFK);

            builder.Property(y => y.IpAddress).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.SessionId).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.LogOutDate).IsRequired(false).HasColumnType("varchar");
        }
    }
}