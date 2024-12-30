using Core.Application.Dtos;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetList;

public class GetListPartTimeWorkerForeignLanguageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPartTimeWorkerFK { get; set; }
    //public PartTimeWorker PartTimeWorkerFK { get; set; }
    public Guid GidForeignLanguageFK { get; set; }
    public string ForeignLanguageFKName { get; set; }



}