using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class Tyre : BaseEntity
    {
        public Guid GidTyreTypeFK { get; set; }
        public TyreType TyreTypeFK { get; set; }
        public string TyreNo { get; set; } = string.Empty;
        public int? ProductionYear { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public EnumTyreStatus TyreStatus { get; set; }
        public string? Description { get; set; }
    }
}
