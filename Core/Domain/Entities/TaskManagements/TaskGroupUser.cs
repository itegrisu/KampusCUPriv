using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.TaskManagements
{
    public class TaskGroupUser : BaseEntity
    {
        public Guid GidTaskGroupFK { get; set; }
        public TaskGroup TaskGroupFK { get; set; }
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
    }
}