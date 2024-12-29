using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Create;

public class CreatedGuestAccommodationRoomResponse : BaseResponse, IResponse
{
    public GetByGidGuestAccommodationRoomResponse Obj { get; set; }
}