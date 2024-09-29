using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetList;

public class GetListRoomTypeListItemDto : IDto
{
    public Guid Gid { get; set; }

    public string Name { get; set; }
    public string Code { get; set; }
    public int Capacity { get; set; }
    public string? Description { get; set; }


}