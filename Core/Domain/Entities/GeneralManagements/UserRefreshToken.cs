using Core.Entities;

namespace Domain.Entities.GeneralManagements
{
    public class UserRefreshToken : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
