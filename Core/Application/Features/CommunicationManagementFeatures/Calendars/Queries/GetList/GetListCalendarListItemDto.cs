using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.CommunicationFeatures.Calendars.Queries.GetList;

public class GetListCalendarListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidEventFK { get; set; }
    public string EventFKName { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string? Color { get; set; }
}