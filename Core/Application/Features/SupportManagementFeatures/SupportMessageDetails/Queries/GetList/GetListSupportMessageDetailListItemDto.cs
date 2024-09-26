using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetList;

public class GetListSupportMessageDetailListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidMessageFK { get; set; }
    public string SupportMessageFKMessage { get; set; }
    public Guid GidReadUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public DateTime ReadDate { get; set; }
    public string ReadIp { get; set; }


}