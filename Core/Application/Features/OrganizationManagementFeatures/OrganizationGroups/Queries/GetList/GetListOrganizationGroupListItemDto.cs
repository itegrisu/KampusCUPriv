using Core.Application.Dtos;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetList;

public class GetListOrganizationGroupListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidOrganizationFK { get; set; }
    public string OrganizationFKOrganizationName { get; set; }

    public string GroupName { get; set; }
    public int RowNo { get; set; }


}