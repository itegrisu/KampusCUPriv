using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGuestGidForPerson
{
    public class GetByGuestGidForPersonListGuestAccommodationResultListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidGuestAccommodationPersonFK { get; set; }
        public string GuestAccommodationPersonFKFullName { get; set; }
        public List<string>? AccommodationInfo { get; set; } // "Konaklama Bilgisi"
        public DateTime GuestAccommodationRoomFKDate { get; set; } // "Tarih"
        public Guid GidGuestAccommodationRoomFK { get; set; }
        public string GuestAccommodationRoomFKRoomTypeFKName { get; set; }
        public int TotalDateCount { get; set; } // "Toplam Konaklanan Gun Sayisi"
    }
}
