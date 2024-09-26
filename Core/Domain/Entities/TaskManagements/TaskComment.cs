using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.TaskManagements
{
    public class TaskComment : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidTaskFK { get; set; }
        public Task TaskFK { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
