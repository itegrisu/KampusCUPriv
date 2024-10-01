using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.OrganizationManagements
{
    public class OrganizationItem : BaseEntity, IHasRowNo
    {
        public Guid GidOrganizationGroupFK { get; set; }
        public OrganizationGroup OrganizationGroupFK { get; set; }
        public Guid? GidMainResponsibleUserFK { get; set; }
        public User? MainResponsibleUserFK { get; set; }

        public string ItemName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Priority { get; set; }
        public bool IsStar { get; set; }
        public EnumItemStatus ItemStatus { get; set; }
        public int RowNo { get; set; }


    }
}
