using Core.Entities;
using Domain.Entities.DefinitionManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AccommodationManagements
{
    public class GuestAccommodationPerson : BaseEntity
    {
        public Guid GidGuestAccommodationFK { get; set; }
        public GuestAccommodation GuestAccommodationFK { get; set; }
        public Guid GidNationalityFK { get; set; }
        public Country CountryFK { get; set; }

        public string? IdNumber { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string? Description { get; set; }

        public ICollection<GuestAccommodationResult>? GuestAccommodationResults { get; set; }
    }
}
