using Core.Entities;
using Domain.Entities.FinanceManagements;
using Domain.Entities.OrganizationManagements;
using Domain.Entities.TransportationManagements;
using Domain.Entities.VehicleManagements;
using Domain.Enums;

namespace Domain.Entities.SupplierCustomerManagements
{
    public class SCCompany : BaseEntity
    {

        public string CompanyName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? WebSite { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool WebLoginStatus { get; set; }
        public string? Description { get; set; }
        public string? SpecialNote { get; set; }
        public string? TaxOffice { get; set; }
        public string? TaxNumber { get; set; }
        public string? Keywords { get; set; }
        public EnumPartnerType PartnerType { get; set; }
        public int? SupplierRank { get; set; }
        public int? CustomerRank { get; set; }
        public bool IsHotel { get; set; }
        public EnumType Type { get; set; }
        public EnumStatus Status { get; set; }

        public ICollection<SCAddress>? SCAddresses { get; set; }
        public ICollection<SCBank>? SCBanks { get; set; }
        public ICollection<SCEmployer>? SCEmployers { get; set; }
        public ICollection<SCWorkHistory>? SCWorkHistories { get; set; }
        public ICollection<Organization> Organizations { get; set; }
        public ICollection<SCPersonnel>? SCPersonnels { get; set; }
        public ICollection<VehicleTransaction>? VehicleTransactions { get; set; }
        public ICollection<FinanceBalance>? FinanceBalances { get; set; }
        public ICollection<TransportationExternalService>? TransportationExternalServices { get; set; }
    }
}