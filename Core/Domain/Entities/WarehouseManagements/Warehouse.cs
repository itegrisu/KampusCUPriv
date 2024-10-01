using Core.Entities;
using Domain.Entities.OrganizationManagements;
using Domain.Enums;

namespace Domain.Entities.WarehouseManagements
{
    public class Warehouse : BaseEntity
    {

        public Guid? GidOrganizationFK { get; set; }
        public Organization? OrganizationFK { get; set; }
        public string Name { get; set; } = string.Empty;
        public EnumWarehouseType WarehouseType { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public ICollection<StockMovement>? PreviousStockMovements { get; set; }
        public ICollection<StockMovement>? NextStockMovements { get; set; }


    }
}
