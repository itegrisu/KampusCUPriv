using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.Guests.Commands.Update;

public class UpdatedGuestResponse : BaseResponse, IResponse
{
    public GetByGidGuestResponse Obj { get; set; }
}