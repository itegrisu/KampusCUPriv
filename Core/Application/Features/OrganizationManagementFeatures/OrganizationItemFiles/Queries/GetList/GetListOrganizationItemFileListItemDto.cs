using Core.Application.Dtos;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetList;

public class GetListOrganizationItemFileListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidOrganizationItemFK { get; set; }
    public string OrganizationItemFKItemName { get; set; }

    public string Title { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }
    public int RowNo { get; set; }


}