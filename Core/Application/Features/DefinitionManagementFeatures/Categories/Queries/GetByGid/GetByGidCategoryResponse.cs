using Core.Application.Responses;

namespace Application.Features.DefinitionFeatures.Categories.Queries.GetByGid
{
    public class GetByGidCategoryResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
    }
}