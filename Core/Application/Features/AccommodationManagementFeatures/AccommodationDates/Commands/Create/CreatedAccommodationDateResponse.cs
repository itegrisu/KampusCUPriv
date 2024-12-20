using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Create;

public class CreatedAccommodationDateResponse : BaseResponse, IResponse
{
    public GetByGidAccommodationDateResponse Obj { get; set; }
}