using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Update;

public class UpdatedReservationHotelResponse : BaseResponse, IResponse
{
    public GetByGidReservationHotelResponse Obj { get; set; }
}