using Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.TaskManagements
{
    public class TaskGroupConfiguration : IEntityTypeConfiguration<TaskGroup>
    {
        public void Configure(EntityTypeBuilder<TaskGroup> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.GroupName).IsRequired().HasColumnType("varchar").HasMaxLength(100);

            builder.HasMany(u => u.TaskGroupUsers).WithOne(y => y.TaskGroupFK).HasForeignKey(y => y.GidTaskGroupFK);

        }
    }
}