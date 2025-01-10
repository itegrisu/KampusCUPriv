using Core.Application.Responses;

namespace Application.Features.CommunicationFeatures.Calendars.Queries.GetByGid
{
    public class GetByGidCalendarResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidEventFK { get; set; }
        public string EventFKName { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string? Color { get; set; }
    }
}