using Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.TaskManagements
{
    public class TaskGroupUserConfiguration : IEntityTypeConfiguration<TaskGroupUser>
    {
        public void Configure(EntityTypeBuilder<TaskGroupUser> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.TaskGroupFK).WithMany(u => u.TaskGroupUsers).HasForeignKey(y => y.GidTaskGroupFK);
            builder.HasOne(y => y.UserFK).WithMany(u => u.TaskGroupUsers).HasForeignKey(y => y.GidUserFK);



        }
    }
}
