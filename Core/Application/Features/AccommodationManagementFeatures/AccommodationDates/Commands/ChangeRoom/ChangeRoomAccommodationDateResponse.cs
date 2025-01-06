using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.ChangeRoom
{
    public class ChangeRoomAccommodationDateResponse : BaseResponse, IResponse
    {
        public GetByGidAccommodationDateResponse Obj { get; set; }
    }
}
