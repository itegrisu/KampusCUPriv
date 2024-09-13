using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetList;

public class GetListLogEmailSendListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKFullName { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }


}