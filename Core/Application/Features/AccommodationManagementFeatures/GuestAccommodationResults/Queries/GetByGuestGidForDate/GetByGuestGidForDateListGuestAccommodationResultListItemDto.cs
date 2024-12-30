using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGuestGidForDate
{
    public class GetByGuestGidForDateListGuestAccommodationResultListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public DateTime Date { get; set; } // "Tarih"
        public Guid GidGuestAccommodationRoomFK { get; set; }
        public string GuestAccommodationRoomFKRoomTypeFKName { get; set; }
        public Guid GidGuestAccommodationPersonFK { get; set; }
        public string GuestAccommodationPersonFKFullName { get; set; }
        public string Count { get; set; } // "Sayi"
        public List<string>? Persons { get; set; } // "Kisiler"
    }
}
