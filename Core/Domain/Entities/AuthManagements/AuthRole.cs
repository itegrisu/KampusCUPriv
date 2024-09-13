using Core.Entities;

namespace Domain.Entities.AuthManagements
{
    public class AuthRole : BaseEntity, IHasRowNo
    {
        public string RoleName { get; set; } = string.Empty;
        public string? RoleDescription { get; set; }
        public string? IconImage { get; set; }
        public int RowNo { get; set; }

        public ICollection<AuthRolePage>? AuthRolePages { get; set; }
        public ICollection<AuthUserRole>? AuthUserRoles { get; set; }

    }
}
