using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Create;

public class CreatedGuestAccommodationResultResponse : BaseResponse, IResponse
{
    public GetByGidGuestAccommodationResultResponse Obj { get; set; }
}