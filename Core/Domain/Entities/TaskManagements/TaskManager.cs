using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.TaskManagements
{
    public class TaskManager : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
    }
}