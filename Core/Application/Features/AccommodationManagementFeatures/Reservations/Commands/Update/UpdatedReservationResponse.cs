using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Commands.Update;

public class UpdatedReservationResponse : BaseResponse, IResponse
{
    public GetByGidReservationResponse Obj { get; set; }
}