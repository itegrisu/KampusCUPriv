using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Create;

public class CreatedReservationHotelStaffResponse : BaseResponse, IResponse
{
    public GetByGidReservationHotelStaffResponse Obj { get; set; }
}