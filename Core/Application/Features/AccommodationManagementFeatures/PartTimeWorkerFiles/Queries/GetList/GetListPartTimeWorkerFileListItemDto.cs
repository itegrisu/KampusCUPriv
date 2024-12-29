using Core.Application.Dtos;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetList;

public class GetListPartTimeWorkerFileListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPartTimeWorkerFK { get; set; }
    //public PartTimeWorker PartTimeWorkerFK { get; set; }

    public string Title { get; set; }
    public string? WorkerFile { get; set; }
    public DateTime ExpiredDate { get; set; }


}