using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Update;

public class UpdatedGuestAccommodationPersonResponse : BaseResponse, IResponse
{
    public GetByGidGuestAccommodationPersonResponse Obj { get; set; }
}