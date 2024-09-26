using Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.TaskManagements
{
    public class TaskUserConfiguration : IEntityTypeConfiguration<TaskUser>
    {
        public void Configure(EntityTypeBuilder<TaskUser> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.TaskUsers).HasForeignKey(y => y.GidUserFK);
            builder.HasOne(y => y.TaskFK).WithMany(u => u.TaskUsers).HasForeignKey(y => y.GidTaskFK);

            builder.Property(y => y.StatusNote).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}