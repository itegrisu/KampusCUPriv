using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetByEnum
{
    public class GetByEnumUserListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid? GidNationalityFK { get; set; }
        public string CountryFKName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Title { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string SifreGuncellemeToken { get; set; }
        public DateTime? TokenExpiredDate { get; set; }
        public string? Avatar { get; set; }
        public bool IsLoginStatus { get; set; }
        public bool IsSystemAdmin { get; set; }
        public string Gsm { get; set; }
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
        public EnumWorkType WorkType { get; set; }
        public EnumEmailActivationStatus EmailActivationStatus { get; set; }
        public EnumSmsActivationStatus SmsActivationStatus { get; set; }
        public string? PersonnelSpecialNote { get; set; }
        public bool IsActive { get; set; }
    }
}
