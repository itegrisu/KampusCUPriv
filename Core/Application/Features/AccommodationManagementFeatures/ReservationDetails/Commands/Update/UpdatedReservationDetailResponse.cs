using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Update;

public class UpdatedReservationDetailResponse : BaseResponse, IResponse
{
    public GetByGidReservationDetailResponse Obj { get; set; }
}