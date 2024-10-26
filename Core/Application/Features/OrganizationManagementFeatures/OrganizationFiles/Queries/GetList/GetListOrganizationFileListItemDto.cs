using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetList;

public class GetListOrganizationFileListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidOrganizationFK { get; set; }
    public string OrganizationFKOrganizationName { get; set; }

    public string Title { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }
    public int RowNo { get; set; }


}