using Core.Application.Dtos;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetList;

public class GetListOrganizationItemMessageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidOrganizationItemFK { get; set; }
    public string OrganizationItemFKItemName { get; set; }
    public Guid GidSendMessageUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Message { get; set; }


}