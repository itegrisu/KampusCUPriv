using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetByGid
{
    public class GetByGidPartTimeWorkerFileResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPartTimeWorkerFK { get; set; }
        public string PartTimeWorkerFKFullName { get; set; }

        public string Title { get; set; }
        public string? WorkerFile { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
}