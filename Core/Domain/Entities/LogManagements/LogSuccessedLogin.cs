using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.LogManagements
{
    public class LogSuccessedLogin : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public string? IpAddress { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public DateTime? LogOutDate { get; set; }
    }
}
