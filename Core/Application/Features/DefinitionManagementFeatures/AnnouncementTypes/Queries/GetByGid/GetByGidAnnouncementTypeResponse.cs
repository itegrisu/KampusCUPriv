using Core.Application.Responses;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetByGid
{
    public class GetByGidAnnouncementTypeResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
    }
}