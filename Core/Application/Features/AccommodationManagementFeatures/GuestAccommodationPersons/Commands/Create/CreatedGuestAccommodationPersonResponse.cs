using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Create;

public class CreatedGuestAccommodationPersonResponse : BaseResponse, IResponse
{
    public GetByGidGuestAccommodationPersonResponse Obj { get; set; }
}