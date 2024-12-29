using Core.Application.Responses;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGid
{
    public class GetByGidGuestAccommodationResultResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidGuestAccommodationPersonFK { get; set; }
        public string GuestAccommodationPersonFKFullName { get; set; }
        public Guid GidGuestAccommodationRoomFK { get; set; }
        public string GuestAccommodationRoomFKRoomTypeFKName { get; set; }
        public string? Note { get; set; }
    }
}