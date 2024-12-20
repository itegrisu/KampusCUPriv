using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Create;

public class CreatedReservationRoomResponse : BaseResponse, IResponse
{
    public GetByGidReservationRoomResponse Obj { get; set; }
}