using Core.Entities;

namespace Domain.Entities.AccommodationManagements
{
    public class PartTimeWorkerFile : BaseEntity
    {

        public Guid GidPartTimeWorkerFK { get; set; }
        public PartTimeWorker PartTimeWorkerFK { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? WorkerFile { get; set; }
        public DateTime? ExpiredDate { get; set; }


    }
}
