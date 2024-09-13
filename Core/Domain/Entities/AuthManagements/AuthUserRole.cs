using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.AuthManagements
{
    public class AuthUserRole : BaseEntity, IHasRowNo
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public Guid? GidRoleFK { get; set; }
        public AuthRole? AuthRoleFK { get; set; }
        public Guid? GidPageFK { get; set; }
        public AuthPage? AuthPageFK { get; set; }
        public int RowNo { get; set; }
    }
}
