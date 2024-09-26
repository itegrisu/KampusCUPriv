using Core.Entities;

namespace Domain.Entities.TaskManagements
{
    public class TaskGroup : BaseEntity
    {
        public string GroupName { get; set; } = string.Empty;
        public ICollection<TaskGroupUser>? TaskGroupUsers { get; set; }
    }
}
