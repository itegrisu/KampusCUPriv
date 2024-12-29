using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Update;

public class UpdatedGuestAccommodationResultResponse : BaseResponse, IResponse
{
    public GetByGidGuestAccommodationResultResponse Obj { get; set; }
}