using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.Guests.Commands.Create;

public class CreatedGuestResponse : BaseResponse, IResponse
{
    public GetByGidGuestResponse Obj { get; set; }
}