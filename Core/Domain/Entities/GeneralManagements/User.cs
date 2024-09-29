using Core.Entities;
using Domain.Entities.AnnouncementManagements;
using Domain.Entities.AuthManagements;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.LogManagements;
using Domain.Entities.PersonnelManagements;
using Domain.Entities.SupportManagements;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using T = Domain.Entities.TaskManagements;

namespace Domain.Entities.GeneralManagements
{
    public class User : BaseEntity
    {
        public Guid? GidNationalityFK { get; set; }
        public Country? CountryFK { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string UpdatePasswordToken { get; set; } = string.Empty;
        public DateTime? TokenExpiredDate { get; set; }
        public bool IsLoginStatus { get; set; }
        public bool IsSystemAdmin { get; set; }
        public string Gsm { get; set; } = string.Empty;
        public string? Birthplace { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? IdentityNo { get; set; }
        public string? PassportNo { get; set; }
        public string? SGKNo { get; set; }
        public string? DrivingLicenseNo { get; set; }
        public string? Note { get; set; }
        public EnumMaritalStatus? MaritalStatus { get; set; }
        public EnumBloodGroup? BloodGroup { get; set; }
        public EnumGender Gender { get; set; }
        //public EnumWorkType WorkType { get; set; }
        public EnumEmailActivationStatus EmailActivationStatus { get; set; }
        public EnumSmsActivationStatus SmsActivationStatus { get; set; }
        public string? PersonnelSpecialNote { get; set; }
        public ICollection<LogSuccessedLogin>? LogSuccessedLogins { get; set; }
        public ICollection<LogAuthorizationError>? LogAuthorizationErrors { get; set; }
        public ICollection<LogUserPageVisit>? LogUserPageActions { get; set; }
        public ICollection<LogUserPageVisitAction>? LogUserPageActionDetails { get; set; }
        public ICollection<LogEmailSend>? LogEmailSends { get; set; }
        public ICollection<AuthUserRole>? AuthUserRoles { get; set; }
        public ICollection<UserRefreshToken>? UserRefreshTokens { get; set; }
        public ICollection<Department> AsilYonetilenDepartmants { get; set; }
        public ICollection<Department> YedekYonetilenDepartmants { get; set; }
        public ICollection<DepartmentUser> DepartmentUsers { get; set; }
        public ICollection<PersonnelAddress> PersonnelAddresses { get; set; }
        public ICollection<PersonnelWorkingTable> PersonnelWorkingTables { get; set; }
        public ICollection<PersonnelPermitInfo> PersonnelPermitInfos { get; set; }
        public ICollection<PersonnelForeignLanguage> PersonnelForeignLanguages { get; set; }
        public ICollection<PersonnelGraduatedSchool> PersonnelGraduatedSchools { get; set; }
        public ICollection<PersonnelPassportInfo> PersonnelPassportInfos { get; set; }
        public ICollection<PersonnelResidenceInfo> PersonnelResidenceInfos { get; set; }
        public ICollection<PersonnelDocument> PersonnelDocuments { get; set; }
        public ICollection<T.Task> Tasks { get; set; }
        public ICollection<T.TaskComment> TaskComments { get; set; }
        public ICollection<T.TaskFile> TaskFiles { get; set; }
        public ICollection<T.TaskGroupUser> TaskGroupUsers { get; set; }
        public ICollection<T.TaskUser> TaskUsers { get; set; }
        public ICollection<T.TaskManager> TaskManagers { get; set; }
        public ICollection<AnnouncementRecipient> AnnouncementRecipients { get; set; }
        public ICollection<SupportMessage> SupportMessages { get; set; }
        public ICollection<SupportMessageDetail> SupportMessageDetails { get; set; }
        public ICollection<UserShortCut> UserShortCuts { get; set; }
        public ICollection<SupportRequest> SupportRequests { get; set; }
        public ICollection<UserModuleAuth> UserModuleAuths { get; set; }


        #region NotMapped alanlar 
        [NotMapped]
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }


        [NotMapped]
        public string FullPhone
        {
            get
            {
                if (Gsm == null)
                    return "";

                if (Gsm.Length != 10)
                    return Gsm;

                return "(" + Gsm.Substring(0, 3) + ") " + Gsm.Substring(3, 3) + " " + Gsm.Substring(6, 2) + " " + Gsm.Substring(8, 2);
            }
        }

        #endregion
    }
}
