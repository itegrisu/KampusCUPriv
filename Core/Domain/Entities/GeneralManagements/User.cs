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
        public Guid? GidUyrukFK { get; set; }
        public Country? CountryFK { get; set; }
        public string Adi { get; set; } = string.Empty;
        public string Soyadi { get; set; } = string.Empty;
        public string EPosta { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
        public string? Unvani { get; set; }
        public string Sifre { get; set; } = string.Empty;
        public string SifreHash { get; set; } = string.Empty;
        public string SifreGuncellemeToken { get; set; } = string.Empty;
        public DateTime? TokenGecerlilikSuresi { get; set; }
        public string? ProfilResmi { get; set; }
        public bool AktifHesapMi { get; set; }
        public bool SistemAdminMi { get; set; }
        public string Gsm { get; set; } = string.Empty;
        public string? DogumYeri { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string? KimlikNo { get; set; }
        public string? PasaportNo { get; set; }
        public string? SGKNo { get; set; }
        public string? EhliyetNo { get; set; }
        public string? Not { get; set; }
        public EnumMedeniDurumu? MedeniDurumu { get; set; }
        public EnumKanGrubu? KanGrubu { get; set; }
        public EnumCinsiyet Cinsiyet { get; set; }
        public EnumEMailAktivasyonDurumu EMailAktivasyonDurumu { get; set; }
        public EnumSmsAktivasyonDurumu SmsAktivasyonDurumu { get; set; }
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


        #region NotMapped alanlar 
        [NotMapped]
        public string TamAd
        {
            get
            {
                return Adi + " " + Soyadi;
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
