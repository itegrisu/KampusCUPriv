using Core.Application.Dtos;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByOrganizationGid
{
    public class GetByOrganizationGidListOrganizationGroupListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidOrganizationFK { get; set; }
        public string OrganizationFKOrganizationName { get; set; }
        public DateTime OrganizationFKStartDate { get; set; }
        public DateTime OrganizationFKEndDate { get; set; }
        public string GroupName { get; set; }
        public int RowNo { get; set; }
    }
}
