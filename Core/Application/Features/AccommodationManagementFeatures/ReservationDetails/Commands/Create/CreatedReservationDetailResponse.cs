using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Create;

public class CreatedReservationDetailResponse : BaseResponse, IResponse
{
    public GetByGidReservationDetailResponse Obj { get; set; }
}