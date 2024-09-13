using Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.PersonnelManagements
{
    public class PersonnelWorkingTableConfiguration : IEntityTypeConfiguration<PersonnelWorkingTable>
    {
        public void Configure(EntityTypeBuilder<PersonnelWorkingTable> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelWorkingTables).HasForeignKey(y => y.GidPersonelFK);

            builder.Property(y => y.IseBaslamaTarihi).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.IstenCikisTarihi).IsRequired(false).HasColumnType("datetime");


        }
    }
}
