using Core.Entities;

namespace Domain.Entities.OrganizationManagements
{
    public class OrganizationItemFile : BaseEntity, IHasRowNo
    {

        public Guid GidOrganizationItemFK { get; set; }
        public OrganizationItem OrganizationItemFK { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Document { get; set; }
        public string? Description { get; set; }
        public int RowNo { get; set; }


    }
}
