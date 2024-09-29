using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetByGid
{
    public class GetByGidOtoBrandResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
    }
}