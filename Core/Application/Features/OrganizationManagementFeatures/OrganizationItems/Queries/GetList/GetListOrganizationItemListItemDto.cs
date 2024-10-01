using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetList;

public class GetListOrganizationItemListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidOrganizationGroupFK { get; set; }
    public string OrganizationGroupFKFullName { get; set; }
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