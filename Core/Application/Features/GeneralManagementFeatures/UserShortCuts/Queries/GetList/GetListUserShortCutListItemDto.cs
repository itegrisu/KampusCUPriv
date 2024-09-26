using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetList;

public class GetListUserShortCutListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public string PageName { get; set; }
    public string PageUrl { get; set; }
    public int RowNo { get; set; }


}