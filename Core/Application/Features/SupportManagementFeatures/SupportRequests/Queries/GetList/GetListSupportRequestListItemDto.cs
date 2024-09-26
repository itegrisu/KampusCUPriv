using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetList;

public class GetListSupportRequestListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Title { get; set; }
    public string UserFKFullName { get; set; }
    public string UserFKAvatar { get; set; }
    public EnumSupportStatus SupportStatus { get; set; }
    public EnumPriorityType PriorityType { get; set; }
    public EnumSupportType SupportType { get; set; }
    public DateTime CreatedDate { get; set; }

}