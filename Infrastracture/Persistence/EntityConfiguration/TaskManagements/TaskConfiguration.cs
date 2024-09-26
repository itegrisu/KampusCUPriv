using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using T = Domain.Entities.TaskManagements;

namespace Persistence.EntityConfiguration.TaskManagements
{
    public class TaskConfiguration : IEntityTypeConfiguration<T.Task>
    {
        public void Configure(EntityTypeBuilder<T.Task> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.Tasks).HasForeignKey(y => y.GidTaskAssignerUserFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.EndDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Description).IsRequired().HasColumnType("varchar").HasMaxLength(500);

            builder.HasMany(u => u.TaskUsers).WithOne(y => y.TaskFK).HasForeignKey(y => y.GidTaskFK);
            builder.HasMany(u => u.TaskComments).WithOne(y => y.TaskFK).HasForeignKey(y => y.GidTaskFK);
            builder.HasMany(u => u.TaskFiles).WithOne(y => y.TaskFK).HasForeignKey(y => y.GidTaskFK);

        }
    }
}