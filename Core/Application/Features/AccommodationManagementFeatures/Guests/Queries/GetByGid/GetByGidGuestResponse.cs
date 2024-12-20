using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByGid
{
    public class GetByGidGuestResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid? GidNationalityFK { get; set; }
        public string CountryFKName { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string? Duty { get; set; }
        public string? Institution { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public EnumGender Gender { get; set; }
        public string? HesCode { get; set; }
        public string? Description { get; set; }
    }
}