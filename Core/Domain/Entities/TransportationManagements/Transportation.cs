using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.FinanceManagements;
using Domain.Entities.OrganizationManagements;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Enums;

namespace Domain.Entities.TransportationManagements
{
    public class Transportation : BaseEntity
    {
        public Guid? GidOrganizationFK { get; set; }
        public Organization? OrganizationFK { get; set; }
        public Guid GidFeeCurrencyFK { get; set; }
        public Currency FeeCurrencyFK { get; set; }
        //public string CustomerInfo { get; set; } = string.Empty;
        public Guid GidCustomerFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public string TransportationNo { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Fee { get; set; }
        public EnumTransportationStatus TransportationStatus { get; set; }

        public ICollection<TransportationService>? TransportationServices { get; set; }
        public ICollection<FinanceBalance>? FinanceBalances { get; set; }
    }
}
