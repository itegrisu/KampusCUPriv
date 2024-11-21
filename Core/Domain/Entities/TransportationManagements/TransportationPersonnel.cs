using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TransportationManagements
{
    public class TransportationPersonnel : BaseEntity
    {
        public Guid? GidTransportationServiceFK { get; set; }
        public TransportationService? TransportationServiceFK { get; set; }
        public Guid GidStaffPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public EnumStaffType StaffType { get; set; }
        public string? Description { get; set; }


    }
}
