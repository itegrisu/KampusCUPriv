using Domain.Entities.SupportManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.SupportManagements
{
    public class SupportMessageConfiguration : IEntityTypeConfiguration<SupportMessage>
    {
        public void Configure(EntityTypeBuilder<SupportMessage> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SupportRequestFK).WithMany(u => u.SupportMessages).HasForeignKey(y => y.GidSupportFK);
            builder.HasOne(y => y.UserFK).WithMany(u => u.SupportMessages).HasForeignKey(y => y.GidSenderUserFK);
            builder.HasMany(builder => builder.SupportMessageDetails).WithOne(detail => detail.SupportMessageFK).HasForeignKey(builder => builder.GidMessageFK);


        

            builder.Property(y => y.Message).IsRequired().HasColumnType("varchar").HasMaxLength(1000);


        }
    }
}
