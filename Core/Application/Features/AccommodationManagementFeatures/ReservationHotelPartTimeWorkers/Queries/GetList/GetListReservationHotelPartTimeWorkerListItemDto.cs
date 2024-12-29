using Core.Application.Dtos;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetList;

public class GetListReservationHotelPartTimeWorkerListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidHotelFK { get; set; }
    public ReservationHotel ReservationHotelFK { get; set; }
    public Guid GidPartTimeWorkerFK { get; set; }
    public PartTimeWorker PartTimeWorkerFK { get; set; }

    public bool IsActive { get; set; }
    public string? Note { get; set; }


}