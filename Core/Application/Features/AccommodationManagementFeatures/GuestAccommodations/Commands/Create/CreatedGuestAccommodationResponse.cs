using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Create;

public class CreatedGuestAccommodationResponse : BaseResponse, IResponse
{
    public GetByGidGuestAccommodationResponse Obj { get; set; }
}