using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.FinanceManagements;
using Domain.Entities.GeneralManagements;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleTransaction : BaseEntity
    {  
        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }
        public Guid? GidSupplierCustomerFK { get; set; }
        public SCCompany? SCCompanyFK { get; set; }
        public Guid? GidVehicleUsePersonnelFK { get; set; }
        public User? UserFK { get; set; }
        public Guid? GidFeeCurrencyFK { get; set; }
        public Currency? CurrencyFK { get; set; }
        public int StartKM { get; set; }
        public int? EndKM { get; set; }
        public int? Fee { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? ArventoAPIInfo { get; set; }
        public string? LicenseFile { get; set; }
        public string? ContractFile { get; set; }
        public string? Description { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EnumVehicleStatus VehicleStatus { get; set; }

        public ICollection<FinanceBalance>? FinanceBalances { get; set; }
    }
}
