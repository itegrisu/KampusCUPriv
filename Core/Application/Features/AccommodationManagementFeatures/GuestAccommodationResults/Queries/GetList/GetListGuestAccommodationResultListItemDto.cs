using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetList;

public class GetListGuestAccommodationResultListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidGuestAccommodationPersonFK { get; set; }
    public string GuestAccommodationPersonFKFullName { get; set; }
    public Guid GidGuestAccommodationRoomFK { get; set; }
    public string GuestAccommodationRoomFKRoomTypeFKName { get; set; }
    public string? Note { get; set; }
}