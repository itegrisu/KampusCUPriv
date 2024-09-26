using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.TaskManagements
{
    public class TaskFile : BaseEntity
    {
        public Guid GidTaskFK { get; set; }
        public Task TaskFK { get; set; }
        public Guid GidFileUploadUserFK { get; set; }
        public User UserFK { get; set; }

        public string FileTitle { get; set; }
        public string? FileDescription { get; set; }
        public string? UploadedFile { get; set; }
    }
}
