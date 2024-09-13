using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Create;

public class CreatedRoomTypeResponse : BaseResponse, IResponse
{
    public GetByGidRoomTypeResponse Obj { get; set; }
}