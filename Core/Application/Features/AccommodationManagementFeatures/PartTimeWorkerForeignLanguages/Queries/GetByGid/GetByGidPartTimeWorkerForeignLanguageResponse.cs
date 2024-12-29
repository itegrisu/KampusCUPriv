using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid
{
    public class GetByGidPartTimeWorkerForeignLanguageResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPartTimeWorkerFK { get; set; }
        //public PartTimeWorker PartTimeWorkerFK { get; set; }
        public Guid GidForeignLanguageFK { get; set; }
        //public ForeignLanguage ForeignLanguageFK { get; set; }


    }
}