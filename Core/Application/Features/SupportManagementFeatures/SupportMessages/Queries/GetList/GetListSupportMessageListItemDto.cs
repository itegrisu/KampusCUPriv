using Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetByGid;
using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetList;

public class GetListSupportMessageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidSupportFK { get; set; }
    public string SupportRequestFKTitle { get; set; }
    public Guid GidSenderUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public string UserFKAvatar { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public EnumMessageType MessageType { get; set; }
    public DataState DataState { get; set; }

    public ICollection<GetByGidSupportMessageDetailResponse> GetByGidSupportMessageDetailResponse  { get; set; }

}