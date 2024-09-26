using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetList;

public class GetListTaskUserListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public Guid GidTaskFK { get; set; }
    public EnumTaskState TaskState { get; set; }
    public string? StatusNote { get; set; }
}