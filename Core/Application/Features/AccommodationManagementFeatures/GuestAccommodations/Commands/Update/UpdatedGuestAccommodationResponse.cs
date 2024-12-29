using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Update;

public class UpdatedGuestAccommodationResponse : BaseResponse, IResponse
{
    public GetByGidGuestAccommodationResponse Obj { get; set; }
}