using Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.TaskManagements
{
    public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
    {
        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.TaskComments).HasForeignKey(y => y.GidUserFK);
            builder.HasOne(y => y.TaskFK).WithMany(u => u.TaskComments).HasForeignKey(y => y.GidTaskFK);

            builder.Property(y => y.Comment).IsRequired().HasColumnType("varchar").HasMaxLength(250);
        }
    }
}