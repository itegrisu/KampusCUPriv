using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.FinanceManagements;
using Domain.Entities.GeneralManagements;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Entities.TransportationManagements;
using Domain.Entities.WarehouseManagements;
using Domain.Enums;

namespace Domain.Entities.OrganizationManagements
{
    public class Organization : BaseEntity
    {

        public Guid GidCustomerFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public Guid GidResponsibleUserFK { get; set; }
        public User ResponsibleUserFK { get; set; }
        public Guid GidOrganizationTypeFK { get; set; }
        public OrganizationType OrganizationTypeFK { get; set; }

        public string OrganizationName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EnumOrganizationStatus OrganizationStatus { get; set; }
        public string? Description { get; set; }

        public ICollection<Warehouse>? Warehouses { get; set; }
        public ICollection<FinanceExpense>? FinanceExpenses { get; set; }
        public ICollection<OrganizationGroup>? OrganizationGroups { get; set; }
        public ICollection<OrganizationFile>? OrganizationFiles { get; set; }
        public ICollection<Transportation>? Transportations { get; set; }
    }
}
