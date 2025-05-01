using Core.Application.Responses;

namespace Application.Features.DefinitionFeatures.Departments.Queries.GetByGid
{
    public class GetByGidDepartmentResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
    }
}