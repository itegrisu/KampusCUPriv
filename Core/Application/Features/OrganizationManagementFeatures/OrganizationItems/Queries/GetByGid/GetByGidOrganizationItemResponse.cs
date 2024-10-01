using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetByGid
{
    public class GetByGidOrganizationItemResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidOrganizationGroupFK { get; set; }
        public string OrganizationGroupFKGroupName { get; set; }
        public Guid? GidMainResponsibleUserFK { get; set; }
        public string UserFKFullName { get; set; }

        public string ItemName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Priority { get; set; }
        public bool IsStar { get; set; }
        public EnumItemStatus ItemStatus { get; set; }
        public int RowNo { get; set; }

    }
}