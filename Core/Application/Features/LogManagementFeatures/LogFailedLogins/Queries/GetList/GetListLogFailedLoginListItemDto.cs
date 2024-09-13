using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetList;

public class GetListLogFailedLoginListItemDto : IDto
{
    public Guid Gid { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public string? IpAddress { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }


}