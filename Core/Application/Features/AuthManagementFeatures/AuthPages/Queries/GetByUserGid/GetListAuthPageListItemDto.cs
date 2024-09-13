using Core.Application.Dtos;


namespace Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByUserGid;

public class GetByUserGidAuthPageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string PageName { get; set; }
    public string RedirectName { get; set; }
    public string PhysicalFilePath { get; set; }
    public string? MenuLink { get; set; }
    public string? PathForAuthCheck { get; set; }
    public bool IsShowMenu { get; set; }
    public string? HelpFileName { get; set; }
    public int RowNo { get; set; }
}