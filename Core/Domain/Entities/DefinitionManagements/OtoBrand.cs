using Core.Entities;
using Domain.Entities.VehicleManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class OtoBrand : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<VehicleAll>? VehicleAlls { get; set; }
    }
}
