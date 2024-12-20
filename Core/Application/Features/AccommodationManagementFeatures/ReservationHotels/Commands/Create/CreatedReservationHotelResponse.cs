using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Create;

public class CreatedReservationHotelResponse : BaseResponse, IResponse
{
    public GetByGidReservationHotelResponse Obj { get; set; }
}