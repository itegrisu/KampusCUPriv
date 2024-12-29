using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGuestGid
{
    public class GetByGuestGidListGuestAccommodationRoomListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidGuestAccommodationFK { get; set; }
        public string GuestAccommodationFKTitle { get; set; }
        public Guid GidRoomTypeFK { get; set; }
        public string RoomTypeFKName { get; set; }
        public DateTime Date { get; set; }
    }
}
