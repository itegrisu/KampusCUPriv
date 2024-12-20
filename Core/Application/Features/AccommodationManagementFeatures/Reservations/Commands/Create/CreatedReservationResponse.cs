using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Commands.Create;

public class CreatedReservationResponse : BaseResponse, IResponse
{
    public GetByGidReservationResponse Obj { get; set; }
}