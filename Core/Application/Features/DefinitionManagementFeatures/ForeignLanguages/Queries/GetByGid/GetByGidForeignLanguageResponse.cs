using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid
{
    public class GetByGidForeignLanguageResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
        public string? LanguageCode { get; set; }

    }
}