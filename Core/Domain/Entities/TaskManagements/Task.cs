using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.TaskManagements
{
    public class Task : BaseEntity
    {
        public Guid GidTaskAssignerUserFK { get; set; }
        public User UserFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public EnumPriorityType PriorityType { get; set; }

        public ICollection<TaskUser>? TaskUsers { get; set; }
        public ICollection<TaskComment>? TaskComments { get; set; }
        public ICollection<TaskFile>? TaskFiles { get; set; }
    }
}
