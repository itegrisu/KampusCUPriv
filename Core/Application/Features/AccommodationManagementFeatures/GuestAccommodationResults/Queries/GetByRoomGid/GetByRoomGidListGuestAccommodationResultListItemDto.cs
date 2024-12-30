using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGuestGid
{
    public class GetByRoomGidListGuestAccommodationResultListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidGuestAccommodationPersonFK { get; set; }
        public string GuestAccommodationPersonFKFullName { get; set; }
        public Guid GidGuestAccommodationRoomFK { get; set; }
        public string GuestAccommodationRoomFKRoomTypeFKName { get; set; }
        public string? Note { get; set; }
    }
}
