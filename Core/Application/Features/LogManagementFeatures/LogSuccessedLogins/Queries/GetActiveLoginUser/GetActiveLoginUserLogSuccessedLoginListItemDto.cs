using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetActiveLoginUser;

public class GetActiveLoginUserLogSuccessedLoginListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public string? IpAddress { get; set; }
    public string SessionId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LogOutDate { get; set; }


}