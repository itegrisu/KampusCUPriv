using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetList;

public class GetListReservationListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid? GidOrganizationFK { get; set; }
    public string OrganizationFKOrganizationName { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int EstimatedGuestCount { get; set; }
    public int EstimatedAccommodationCount { get; set; }
    public EnumReservationType ReservationType { get; set; }
    public EnumReservationStatus ReservationStatus { get; set; }
}