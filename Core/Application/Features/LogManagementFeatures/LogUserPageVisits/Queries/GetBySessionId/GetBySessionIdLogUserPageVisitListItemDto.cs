using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetBySessionId;

public class GetBySessionIdLogUserPageVisitListItemDto  : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public string? IpAddress { get; set; }
    public string PageInfo { get; set; }
    public string SessionId { get; set; }
    public DateTime CreatedDate { get; set; }


}