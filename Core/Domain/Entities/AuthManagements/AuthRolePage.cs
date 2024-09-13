using Core.Entities;

namespace Domain.Entities.AuthManagements
{
    public class AuthRolePage : BaseEntity, IHasRowNo
    {
        public Guid GidRoleFK { get; set; }
        public AuthRole AuthRoleFK { get; set; }
        public Guid GidPageFK { get; set; }
        public AuthPage AuthPageFK { get; set; }
        public int RowNo { get; set; }
    }
}