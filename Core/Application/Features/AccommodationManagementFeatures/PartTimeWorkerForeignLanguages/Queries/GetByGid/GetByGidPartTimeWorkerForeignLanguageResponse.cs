using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid
{
    public class GetByGidPartTimeWorkerForeignLanguageResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPartTimeWorkerFK { get; set; }
        public string PartTimeWorkerFKFullName { get; set; }
        public Guid GidForeignLanguageFK { get; set; }
        public string ForeignLanguageFKName { get; set; }


    }
}