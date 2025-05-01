using Domain.Entities.ClubManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.ClubManagements
{
    public class StudentClubConfiguration : IEntityTypeConfiguration<StudentClub>
    {
        public void Configure(EntityTypeBuilder<StudentClub> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.StudentClubs).HasForeignKey(y => y.GidUserFK);
            builder.HasOne(y => y.ClubFK).WithMany(u => u.StudentClubs).HasForeignKey(y => y.GidClubFK);
        }
    }
}
