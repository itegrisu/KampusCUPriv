using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.OrganizationManagements
{
    public class OrganizationItemMessage : BaseEntity
    {
        public Guid GidOrganizationItemFK { get; set; }
        public OrganizationItem OrganizationItemFK { get; set; }
        public Guid GidSendMessageUserFK { get; set; }
        public User UserFK { get; set; }

        public string Message { get; set; } = string.Empty;

    }
}
