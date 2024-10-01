using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.GeneralManagements
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.CountryFK).WithMany(u => u.Users).HasForeignKey(y => y.GidNationalityFK);

            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Surname).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Email).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Title).IsRequired(false).HasColumnType("varchar").HasMaxLength(60);
            builder.Property(y => y.Password).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            builder.Property(y => y.PasswordHash).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            builder.Property(y => y.UpdatePasswordToken).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.TokenExpiredDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.IsSystemAdmin).IsRequired().HasColumnType("bit");
            builder.Property(y => y.Gsm).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Birthplace).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.BirthDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.IdentityNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.PassportNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.SGKNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.DrivingLicenseNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Note).IsRequired(false).HasColumnType("varchar").HasMaxLength(300);
            builder.Property(builder => builder.MaritalStatus).IsRequired(false).HasColumnType("int");
            builder.Property(builder => builder.BloodGroup).IsRequired(false).HasColumnType("int");
            builder.Property(builder => builder.Gender).IsRequired().HasColumnType("int");
            builder.Property(builder => builder.EmailActivationStatus).IsRequired().HasColumnType("int");
            builder.Property(builder => builder.SmsActivationStatus).IsRequired().HasColumnType("int");
            builder.Property(builder => builder.Avatar).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);

            builder.HasMany(u => u.AsilYonetilenDepartmants).WithOne(y => y.MainAdminFK).HasForeignKey(y => y.GidMainAdminFK);
            builder.HasMany(u => u.YedekYonetilenDepartmants).WithOne(y => y.CoAdminFK).HasForeignKey(y => y.GidCoAdminFK);
            builder.HasMany(u => u.DepartmentUsers).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasMany(u => u.PersonnelAddresses).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasMany(u => u.PersonnelWorkingTables).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasMany(u => u.PersonnelPermitInfos).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasMany(u => u.PersonnelForeignLanguages).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasMany(u => u.PersonnelGraduatedSchools).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasMany(u => u.PersonnelPassportInfos).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasMany(u => u.PersonnelResidenceInfos).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasMany(u => u.PersonnelDocuments).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonnelFK);

            builder.HasMany(u => u.LogSuccessedLogins).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.LogAuthorizationErrors).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.LogUserPageActions).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.LogUserPageActionDetails).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.LogEmailSends).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.AuthUserRoles).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.UserRefreshTokens).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);

            builder.HasMany(u => u.Tasks).WithOne(y => y.UserFK).HasForeignKey(y => y.GidTaskAssignerUserFK);
            builder.HasMany(u => u.TaskComments).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.TaskFiles).WithOne(y => y.UserFK).HasForeignKey(y => y.GidFileUploadUserFK);
            builder.HasMany(u => u.TaskGroupUsers).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.TaskUsers).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.TaskManagers).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);

            builder.HasMany(u => u.AnnouncementRecipients).WithOne(y => y.UserFK).HasForeignKey(y => y.GidRecipientFK);
            builder.HasMany(u => u.SupportMessages).WithOne(y => y.UserFK).HasForeignKey(y => y.GidSenderUserFK);
            builder.HasMany(u => u.SupportMessageDetails).WithOne(y => y.UserFK).HasForeignKey(y => y.GidReadUserFK);
            builder.HasMany(u => u.UserShortCuts).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
            builder.HasMany(u => u.SupportRequests).WithOne(y => y.UserFK).HasForeignKey(y => y.CreatedUserFK);

            builder.HasMany(u => u.Organizations).WithOne(y => y.ResponsibleUserFK).HasForeignKey(y => y.GidResponsibleUserFK);

            builder.HasMany(u => u.SendedFinanceExpenses).WithOne(y => y.MoneySenderPersonnelFK).HasForeignKey(y => y.GidMoneySenderPersonnelFK);
            builder.HasMany(u => u.ReceivedFinanceExpenses).WithOne(y => y.MoneyReceivePersonnelFK).HasForeignKey(y => y.GidMoneyReceivePersonnelFK);
            builder.HasMany(u => u.ApprovedFinanceExpenses).WithOne(y => y.ApprovalReceiverFK).HasForeignKey(y => y.GidApprovalReceiverFK);

            builder.HasMany(u => u.SpendedFinanceExpenseDetails).WithOne(y => y.SpendPersonnelFK).HasForeignKey(y => y.GidSpendPersonnelFK);
            builder.HasMany(u => u.ControlledFinanceExpenseDetails).WithOne(y => y.ControlPersonnelFK).HasForeignKey(y => y.GidControlPersonnelFK);

            builder.HasMany(u => u.UserModuleAuths).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);

            builder.Property(y => y.CreatedDate).IsRequired().HasColumnType("datetime");

            builder.HasMany(y => y.OrganizationItems).WithOne(y => y.MainResponsibleUserFK).HasForeignKey(y => y.GidMainResponsibleUserFK);

        }
    }
}
