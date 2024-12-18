using Core.Entities;
using Domain.Entities.VehicleManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DefinitionManagements
{
    public class TyreType : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public EnumTyreTypeName TyreTypeName { get; set; }
        public string? Size { get; set; }

        public ICollection<VehicleTyre>? Tyres { get; set; }
    }
}
