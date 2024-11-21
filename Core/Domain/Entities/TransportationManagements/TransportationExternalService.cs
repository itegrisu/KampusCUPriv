using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.FinanceManagements;
using Domain.Entities.OrganizationManagements;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TransportationManagements
{
    public class TransportationExternalService : BaseEntity
    {
        public Guid GidSupplierFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public Guid? GidOrganizationFK { get; set; }
        public Organization? OrganizationFK { get; set; }
        public Guid GidFeeCurrencyFK { get; set; }
        public Currency CurrencyFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Fee { get; set; }
        public string PlateNo { get; set; } = string.Empty;
        public EnumExternalVehicleType ExternalVehicleType { get; set; }
        public int? PassengerCapacity { get; set; }
        public string? VehicleOfficer { get; set; }
        public string? VehiclePhone { get; set; }
        public bool? IsHasFile { get; set; }
        public EnumExternalServiceStatus ExternalServiceStatus { get; set; }
        public string? Description { get; set; }

        public ICollection<FinanceBalance>? FinanceBalances { get; set; }
    }
}
