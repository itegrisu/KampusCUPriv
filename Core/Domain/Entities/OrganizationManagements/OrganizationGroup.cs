using Core.Entities;

namespace Domain.Entities.OrganizationManagements
{
    public class OrganizationGroup : BaseEntity, IHasRowNo
    {
        public Guid GidOrganizationFK { get; set; }
        public Organization OrganizationFK { get; set; }

        public string GroupName { get; set; } = string.Empty;
        public int RowNo { get; set; }

        public ICollection<OrganizationItem>? OrganizationItems { get; set; }

    }
}
