using Core.Entities;

namespace Domain.Entities.LogManagements
{
    public class LogFailedLogin : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? IpAddress { get; set; }
        public string? Description { get; set; }
    }
}
