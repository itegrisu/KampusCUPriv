using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetByGid
{
    public class GetByGidJobTypeResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }

    }
}