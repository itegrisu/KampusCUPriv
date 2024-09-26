using Core.Application.Dtos;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetList;

public class GetListTaskGroupListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string GroupName { get; set; }
    public int Count { get; set; }
}