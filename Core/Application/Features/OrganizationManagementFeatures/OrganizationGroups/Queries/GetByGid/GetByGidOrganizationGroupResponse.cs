using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByGid
{
    public class GetByGidOrganizationGroupResponse : IResponse
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