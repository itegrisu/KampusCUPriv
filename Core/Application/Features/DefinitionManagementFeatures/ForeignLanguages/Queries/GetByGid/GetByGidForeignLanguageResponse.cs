using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid
{
    public class GetByGidForeignLanguageResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string DilAdi { get; set; }
        public string? DilKodu { get; set; }

    }
}