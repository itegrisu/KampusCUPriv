using Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.TaskManagements
{
    public class TaskFileConfiguration : IEntityTypeConfiguration<TaskFile>
    {
        public void Configure(EntityTypeBuilder<TaskFile> builder)
        {


            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.TaskFiles).HasForeignKey(y => y.GidFileUploadUserFK);
            builder.HasOne(y => y.TaskFK).WithMany(u => u.TaskFiles).HasForeignKey(y => y.GidTaskFK);

            builder.Property(y => y.FileTitle).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.FileDescription).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.UploadedFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);


        }
    }
}