using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.TaskManagementFeatures.Tasks.Queries.GetList;

public class GetListTaskListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string UserFKGid { get; set; }
    public string UserFKFullName { get; set; }
    public string Title { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public EnumPriorityType PriorityType { get; set; }
    public DataState DataState { get; set; }
    public DateTime CreatedDate { get; set; }
}