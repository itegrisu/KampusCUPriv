using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetByGid
{
    public class GetByGidPermitTypeResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string IzinAdi { get; set; }

    }
}