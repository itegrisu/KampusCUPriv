using Core.Application.Dtos;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetList;

public class GetListTaskGroupUserListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid UserFKGid { get; set; }
    public string UserFKFullName { get; set; }
    public string UserFKIdNumber { get; set; }
    public string UserFKAvatar { get; set; }
}