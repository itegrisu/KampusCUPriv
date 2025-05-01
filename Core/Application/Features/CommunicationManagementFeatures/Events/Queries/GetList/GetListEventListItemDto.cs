using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.Events.Queries.GetList;

public class GetListEventListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidClubFK { get; set; }
    public string ClubFKName { get; set; }
    public string ClubFKColor { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public EnumEventStatus EventStatus { get; set; }
}