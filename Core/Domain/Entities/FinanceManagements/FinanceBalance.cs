using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Entities.TransportationManagements;
using Domain.Entities.VehicleManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FinanceManagements
{
    public class FinanceBalance : BaseEntity
    {
        public Guid GidSupplierCustomerFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public Guid? GidVehicleTransactionFK { get; set; }
        public VehicleTransaction? VehicleTransactionFK { get; set; }
        public Guid? GidTransportationFK { get; set; }
        public Transportation? TransportationFK { get; set; }
        public Guid? GidTransportationExternalServiceFK { get; set; }
        public TransportationExternalService? TransportationExternalServiceFK { get; set; }
        public Guid GidFeeCurrencyFK { get; set; }
        public Currency CurrencyFK { get; set; }
        public EnumBalanceType BalanceType { get; set; }
        public EnumBalanceResourceType BalanceResourceType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Fee { get; set; }
        public EnumPaymentStatus PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentFile { get; set; }
        public string? Description { get; set; }
    }
}
