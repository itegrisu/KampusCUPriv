using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGuestGid
{
    public class GetByGuestGidListGuestAccommodationPersonListItemDto : IDto
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
