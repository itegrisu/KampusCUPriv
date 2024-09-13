using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Update;

public class UpdatedRoomTypeResponse : BaseResponse, IResponse
{
    public GetByGidRoomTypeResponse Obj { get; set; }
}