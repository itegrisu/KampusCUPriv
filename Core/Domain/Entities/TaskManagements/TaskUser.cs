using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.TaskManagements
{
    public class TaskUser : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidTaskFK { get; set; }
        public Task TaskFK { get; set; }
        public EnumTaskState TaskState { get; set; }
        public string? StatusNote { get; set; }
    }
}