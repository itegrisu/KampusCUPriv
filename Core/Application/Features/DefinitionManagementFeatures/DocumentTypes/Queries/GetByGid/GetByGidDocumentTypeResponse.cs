using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetByGid
{
    public class GetByGidDocumentTypeResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string BelgeAdi { get; set; }

    }
}