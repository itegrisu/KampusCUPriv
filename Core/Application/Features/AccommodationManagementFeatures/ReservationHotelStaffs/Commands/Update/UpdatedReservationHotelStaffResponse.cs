using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Update;

public class UpdatedReservationHotelStaffResponse : BaseResponse, IResponse
{
    public GetByGidReservationHotelStaffResponse Obj { get; set; }
}