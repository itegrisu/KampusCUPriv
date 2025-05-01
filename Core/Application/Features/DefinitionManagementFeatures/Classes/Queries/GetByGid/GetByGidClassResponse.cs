using Core.Application.Responses;

namespace Application.Features.DefinitionFeatures.Classes.Queries.GetByGid
{
    public class GetByGidClassResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
    }
}