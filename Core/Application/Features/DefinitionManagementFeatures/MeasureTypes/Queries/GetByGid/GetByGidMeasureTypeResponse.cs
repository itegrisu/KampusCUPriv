using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetByGid
{
    public class GetByGidMeasureTypeResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }

    }
}