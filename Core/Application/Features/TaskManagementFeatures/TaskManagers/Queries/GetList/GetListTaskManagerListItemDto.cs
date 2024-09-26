using Core.Application.Dtos;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetList;

public class GetListTaskManagerListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string UserFKGid { get; set; }
    public string UserFKFullName { get; set; } 
    public string UserFKAvatar { get; set; }
}