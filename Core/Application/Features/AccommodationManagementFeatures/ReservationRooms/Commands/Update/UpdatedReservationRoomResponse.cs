using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Update;

public class UpdatedReservationRoomResponse : BaseResponse, IResponse
{
    public GetByGidReservationRoomResponse Obj { get; set; }
}