using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid
{
    public class GetByGidRoomTypeResponse : IResponse
    {
        public Guid Gid { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }

    }
}