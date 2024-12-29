using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Update;

public class UpdatedGuestAccommodationRoomResponse : BaseResponse, IResponse
{
    public GetByGidGuestAccommodationRoomResponse Obj { get; set; }
}