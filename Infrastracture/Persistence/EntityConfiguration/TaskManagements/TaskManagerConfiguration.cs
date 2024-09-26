using Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.TaskManagements
{
    public class TaskManagerConfiguration : IEntityTypeConfiguration<TaskManager>
    {
        public void Configure(EntityTypeBuilder<TaskManager> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.HasOne(y => y.UserFK).WithMany(u => u.TaskManagers).HasForeignKey(y => y.GidUserFK);

        }
    }
}
