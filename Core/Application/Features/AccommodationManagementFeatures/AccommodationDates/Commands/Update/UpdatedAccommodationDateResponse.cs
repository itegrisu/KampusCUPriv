using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Update;

public class UpdatedAccommodationDateResponse : BaseResponse, IResponse
{
    public GetByGidAccommodationDateResponse Obj { get; set; }
}