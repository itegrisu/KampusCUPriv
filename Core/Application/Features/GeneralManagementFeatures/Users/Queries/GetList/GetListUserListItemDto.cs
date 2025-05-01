using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.GeneralFeatures.Users.Queries.GetList;

public class GetListUserListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid? GidDepartmentFK { get; set; }
    public string DepartmentFKName { get; set; }
    public Guid? GidClassFK { get; set; }
    public string ClassFKName { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsBloodDonor { get; set; }
    public bool IsEmailVerified { get; set; }
    public string DeviceToken { get; set; }
    public bool IsNotificationsEnabled { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}