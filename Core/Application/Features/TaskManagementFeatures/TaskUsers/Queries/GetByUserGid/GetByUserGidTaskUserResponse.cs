using Core.Application.Dtos;
using Domain.Enums;
using T = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByUserGid
{
    public class GetByUserGidTaskUserResponse : IDto
    {
        public Guid Gid { get; set; }
        //  public int GidUserFK { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKGid { get; set; }
        public string TaskFKTitle { get; set; } = string.Empty;
        public DateTime TaskFKEndDate { get; set; }
        public string TaskFKGid { get; set; }
        public string TaskFKDescription { get; set; } = string.Empty;
        public EnumPriorityType TaskFKPriorityType { get; set; }
        public EnumTaskState TaskState { get; set; }
        public string? StatusNote { get; set; }
        public T.Task TaskFK { get; set; }
    }
}
