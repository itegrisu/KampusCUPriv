using Core.Entities;
using Domain.Entities.ClubManagements;
using Domain.Entities.CommunicationManagements;
using Domain.Entities.DefinitionManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.GeneralManagements
{
    public class User : BaseEntity
    {
        public Guid? GidDepartmentFK { get; set; }
        public Department? DepartmentFK { get; set; }
        public Guid? GidClassFK { get; set; }
        public Class? ClassFK { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } 
        public bool? IsBloodDonor { get; set; }
        public bool IsEmailVerified { get; set; }
        public string? EmailVerificationCode { get; set; }
        public DateTime? EmailVerificationCodeExpire { get; set; }
        public string? DeviceToken { get; set; }
        public bool IsNotificationsEnabled { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public ICollection<Club>? Clubs { get; set; }
        public ICollection<StudentClub>? StudentClubs { get; set; }
        public ICollection<StudentAnnouncement>? StudentAnnouncements { get; set; }
    }
}
