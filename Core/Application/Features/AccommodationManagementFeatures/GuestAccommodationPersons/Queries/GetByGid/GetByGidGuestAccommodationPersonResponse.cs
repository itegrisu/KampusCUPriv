using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGid
{
    public class GetByGidGuestAccommodationPersonResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidGuestAccommodationFK { get; set; }
        public string GuestAccommodationFKTitle { get; set; }
        public Guid GidNationalityFK { get; set; }
        public string CountryFKName { get; set; }
        public string? IdNumber { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Description { get; set; }
    }
}