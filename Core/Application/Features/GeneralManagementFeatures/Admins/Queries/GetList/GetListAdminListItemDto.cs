using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.GeneralFeatures.Admins.Queries.GetList;

public class GetListAdminListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidClubFK { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; } = string.Empty;
}