using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetList;

public class GetListUserModuleAuthListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public string UserFKAvatar { get; set; }
    public EnumModuleType ModuleType { get; set; }


}