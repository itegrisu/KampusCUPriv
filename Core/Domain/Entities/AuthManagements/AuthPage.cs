using Core.Entities;

namespace Domain.Entities.AuthManagements
{
    public class AuthPage : BaseEntity, IHasRowNo
    {
        
        public string PageName { get; set; } = string.Empty;
        public string RedirectName { get; set; } = string.Empty;
        public string PhysicalFilePath { get; set; } = string.Empty;
        public string? MenuLink { get; set; }
        public string? PathForAuthCheck { get; set; }
        public bool IsShowMenu { get; set; }
        public string? HelpFileName { get; set; }
        public int RowNo { get; set; }

        public ICollection<AuthRolePage>? AuthRolePages { get; set; }
        public ICollection<AuthUserRole>? AuthUserRoles { get; set; }
    }
}
