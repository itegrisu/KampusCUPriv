using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Queries.GetByServiceGid
{
    public class GetByServiceGidListTransportationPersonnelListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid? GidTransportationServiceFK { get; set; }
        public string TransportationServiceFKServiceNo { get; set; }
        public Guid GidStaffPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public EnumStaffType StaffType { get; set; }
        public EnumStaffStatus StaffStatus { get; set; }
        public string? Description { get; set; }
    }
}
